using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

using LantanaGroup.TestDataGenerationTool.Data;

namespace LantanaGroup.TestDataGenerationTool.Logic
{
    public class BadFileGenerator : FileGenerator 
    {
        /// <summary>
        /// Performs the necessary token replacement in the sample output file
        /// </summary>
        /// <param name="masterFile">The master file containing tokens to replace</param>
        /// <param name="outputLocation">Where to write the files</param>
        /// <param name="dataSourceFile">The file containing the data that will be replaced with in the master file copies</param>
        /// <returns></returns>
        public static int GenerateOutputFiles(FileInfo masterFile, DirectoryInfo outputLocation, FileInfo dataSourceFile)
        {
            SampleDataSet dataSet = null;
            Dictionary<string, int> activeRows = null;
            Dictionary<string, string[]> activeRowsData = null;
            Instance[] instances = null;

            XmlDocument originalDocument = new XmlDocument();
            XmlDocument tempDocument = null;

            string originalContent = null;
            string currentContent = null;
            string outputPath = null;
            int numOutputFiles = 0;
            int numOutputFilesWritten = 0;
            string largestSection = "";

            //Retreive the configuration and the data source
            try
            {
                dataSet = SampleDataSetRetriever.GetFromXLS(dataSourceFile);
            }
            catch (Exception e)
            {
                LogFactory.LogFatal("Error opening data source file: " + e.Message);
            }

            GetOutputFilesCount(dataSet, out numOutputFiles, out largestSection);

            //Open the master file and read it to a string and parse it
            try
            {
                using (StreamReader reader = new StreamReader(masterFile.FullName))
                {
                    originalContent = reader.ReadToEnd();
                }

                originalDocument.LoadXml(originalContent);
            }
            catch (Exception e)
            {
                LogFactory.LogFatal("Error reading and parsing master file: " + e.Message);
            }

            //Generate the output files
            for (int i = 0; i < numOutputFiles; ++i)
            {
                try
                {
                    //Get a new copy of the file content
                    currentContent = originalContent;

                    //Create the output file
                    outputPath = GetOutputFileName(outputLocation, i, masterFile);

                    //Retrieve the active sections for this file
                    activeRows = GetActiveSectionRows(dataSet, i, largestSection);
                    activeRowsData = GetActiveSectionRowsData(dataSet, activeRows);

                    //NOTE: This is the only line that differs between good and bad. Consolidate?
                    instances = GetApplicableInstances(TestDataGenerationTool.ActionConfigurationInstance, activeRows, false);

                    // Only create bad-files if something has defined in the action configuration to make them bad
                    // (Otherwise they are just the same as the master file, which in theory, should be good by default)
                    if (instances.Length > 0)
                    {
                        //Perform content modifications and get resulting string
                        tempDocument = PerformContentModifications(originalDocument,
                            TestDataGenerationTool.ActionConfigurationInstance, activeRows, instances);
                        currentContent = tempDocument.OuterXml;

                        //Tokenize and write the resulting string
                        currentContent = TokenizeString(dataSet, activeRowsData, currentContent);

                        //Validate basic XML well-formedness
                        tempDocument = new XmlDocument();
                        tempDocument.LoadXml(currentContent);

                        // Save using XmlDocument so that it pretty-prints the xml in the output file.
                        tempDocument.Save(outputPath);

                        ++numOutputFilesWritten;
                    }
                }
                catch (XmlException xex)
                {
                    throw new Exception("Resulting file is not valid XML: " + xex);
                }
                catch (Exception e)
                {
                    LogFactory.LogError("Unable to write file " + outputPath + ": " + e);
                }
            }

            return numOutputFilesWritten;
        }
    }
}
