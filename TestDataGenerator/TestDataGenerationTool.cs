using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data.OleDb;

using LantanaGroup.TestDataGenerator.Shared;
using LantanaGroup.TestDataGenerator.Shared.Data;
using LantanaGroup.TestDataGenerator.Shared.Logic;

using NDesk.Options;

namespace LantanaGroup.TestDataGenerator
{
    /// <summary>
    /// A command-line tool that generates a set of given output files based on 
    /// sample input data.
    /// </summary>
    public class TestDataGenerationTool
    {
        #region Main

        public static void Main(string[] args)
        {
            TestDataGenerationTool tool = new TestDataGenerationTool();
            bool optionHelp = false;
            string optionDataSource = string.Empty;
            string optionGoodFiles = string.Empty;
            string optionBadFiles = string.Empty;
            string optionValidationProfile = string.Empty;
            string optionErrorFile = string.Empty;
            int optionMaxOutput = -1;
            string optionActionConfig = string.Empty;
            
            //parse command-lime params
            OptionSet p = new OptionSet()
                .Add("ac|actionconfig:", "The location of the action configuration file.", delegate(string v) { optionActionConfig = v; })
                .Add("g|goodfiles:", "ACTION: Location (directory) where the good files should be output to.", delegate(string v) { optionGoodFiles = v; })
                .Add("b|badfiles:", "ACTION: Location (directory) where the bad files should be output to.", delegate(string v) { optionBadFiles = v; })
                .Add("e|errors:", "Location (file) to where the errors should be logged to.", delegate(string v) { optionErrorFile = v; })
                .Add("mo|maxoutput:", "Maximum number of files to generate.", delegate(string v) { Int32.TryParse(v, out optionMaxOutput); })
                .Add("vc|validatecontent:", "Validates the output content against the NIST online validator using the specified validation profile.", delegate(string v) { optionValidationProfile = v; })
                .Add("h|help", "Display this help information", delegate(string v) { optionHelp = true; });
            List<string> extraParams = p.Parse(args);

#if DEBUG
            System.Console.WriteLine("Attach debugger and press ENTER to continue");
            System.Console.ReadLine();
#endif

            //TODO: Add override for number of output files

            if (optionHelp || (string.IsNullOrEmpty(optionGoodFiles) && string.IsNullOrEmpty(optionBadFiles)))
            {
                Console.WriteLine("-------------------------------------");
                p.WriteOptionDescriptions(Console.Out);
                Console.WriteLine("-------------------------------------\n\nValidation Profiles:");

                try
                {
                    using (NistRemoteValidator.ValidationWebServicePortTypeClient client = new NistRemoteValidator.ValidationWebServicePortTypeClient(Properties.Settings.Default.NistRemoteValidatorEndpoint))
                    {
                        NistRemoteValidator.WSSpecification[] profiles = client.getAvailableValidations();

                        foreach (NistRemoteValidator.WSSpecification cProfile in profiles)
                        {
                            Console.WriteLine("- {0}: {1}", cProfile.specificationId, cProfile.name);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error making profile request to NIST online validator: {0}", ex.Message);
                }

                Console.WriteLine("-------------------------------------\nPress <enter> key to close.");
                Console.ReadLine();

                return;
            }

            try
            {
                tool.GenerateTestData(optionActionConfig, optionGoodFiles, optionBadFiles, optionValidationProfile, optionErrorFile, optionMaxOutput);
            }
            catch (LogException)
            {
                // Do nothing, the error has already been recorded
            }
            catch (Exception ex)
            {
                LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, ex.Message);
            }

#if DEBUG
            Console.WriteLine("Press <enter> key to close.");
            Console.ReadLine();
#endif
        }

        #endregion

        public void GenerateTestData(string actionConfigFile, string goodFilesDirectory, string badFilesDirectory, string validationProfile, string errorFile, int maxOutput)
        {
            ActionConfiguration actionConfig = null;
            string masterFile = string.Empty;
            string dataSource = string.Empty;

            // Parse configuration file
            if (!string.IsNullOrEmpty(actionConfigFile))
            {
                if (!File.Exists(actionConfigFile))
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, actionConfigFile, "The action configuration file specified does not exist!");
                    return;
                }

                actionConfig = ConfigurationFileParser.ParseConfigurationFile(new FileInfo(actionConfigFile), out dataSource, out masterFile);
            }

            // Verify good files directory exists, or try to create it
            if (!string.IsNullOrEmpty(goodFilesDirectory) && !Directory.Exists(goodFilesDirectory))
            {
                try
                {
                    Directory.CreateDirectory(goodFilesDirectory);
                }
                catch (Exception ex)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, goodFilesDirectory, "Could not create good files directory ({0})", ex.Message);
                }
            }
            else if (!string.IsNullOrEmpty(goodFilesDirectory))
            {
                try
                {
                    foreach (string cFile in Directory.GetFiles(goodFilesDirectory))
                    {
                        File.Delete(cFile);
                    }
                }
                catch (Exception ex)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, goodFilesDirectory, "Could not delete files within good directory due to error: {0}", ex.Message);
                }
            }

            // Verify bad files directory exists, or try to create it
            if (!string.IsNullOrEmpty(badFilesDirectory) && !Directory.Exists(badFilesDirectory))
            {
                try
                {
                    Directory.CreateDirectory(badFilesDirectory);
                }
                catch (Exception ex)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, badFilesDirectory, "Could not create bad files directory ({0})", ex.Message);
                }
            }
            else if (!string.IsNullOrEmpty(badFilesDirectory))
            {

                try
                {
                    foreach (string cFile in Directory.GetFiles(badFilesDirectory))
                    {
                        File.Delete(cFile);
                    }
                }
                catch (Exception ex)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, badFilesDirectory, "Could not delete files within bad directory due to error: {0}", ex.Message);
                }
            }

            if (!string.IsNullOrEmpty(goodFilesDirectory) || !string.IsNullOrEmpty(badFilesDirectory))
            {
                // Execute good file generation.
                if (!string.IsNullOrEmpty(goodFilesDirectory))
                {
                    FileInfo masterFileInfo = new FileInfo(masterFile);
                    DirectoryInfo goodFilesDirectoryInfo = new DirectoryInfo(goodFilesDirectory);

                    Console.WriteLine("Preparing to generate GOOD files");
                    FileGenerator.GenerateOutputFiles(actionConfig, masterFileInfo, goodFilesDirectoryInfo, dataSource, true, validationProfile, maxOutput);
                }

                // Execute bad file generation.
                if (!string.IsNullOrEmpty(badFilesDirectory))
                {
                    FileInfo masterFileInfo = new FileInfo(masterFile);
                    DirectoryInfo badFilesDirectoryInfo = new DirectoryInfo(badFilesDirectory);

                    Console.WriteLine("Preparing to generate BAD files");
                    FileGenerator.GenerateOutputFiles(actionConfig, masterFileInfo, badFilesDirectoryInfo, dataSource, false, validationProfile, -1);
                }
            }

            // Save the error log file
            if (!string.IsNullOrEmpty(errorFile))
            {
                LogFactory.SaveDocLog(errorFile);

                string errorTransformLocation = Path.Combine(new FileInfo(errorFile).DirectoryName, "MessageLog.xslt");

                using (StreamReader transformReader = new StreamReader(System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("LantanaGroup.TestDataGenerator.MessageLog.xslt")))
                {
                    File.WriteAllText(errorTransformLocation, transformReader.ReadToEnd());
                }
            }
        }

        public void GenerateTestData(string actionConfig, string goodFilesDirectory, string badFilesDirectory)
        {
            GenerateTestData(actionConfig, goodFilesDirectory, badFilesDirectory, null, string.Empty, -1);
        }
    }
}
