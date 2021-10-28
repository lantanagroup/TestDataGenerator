using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using System.Text.RegularExpressions;

using LantanaGroup.TestDataGenerator.Shared.Data;


namespace LantanaGroup.TestDataGenerator.Shared.Logic
{
    public class FileGenerator
    {
        public const string DefaultValue = "NO VALUE PROVIDED";

        /// <summary>
        /// Performs the necessary token replacement in the sample output file
        /// </summary>
        /// <param name="masterFile">The master file containing tokens to replace</param>
        /// <param name="outputLocation">Where to write the files</param>
        /// <param name="dataSourceFile">The file containing the data that will be replaced with in the master file copies</param>
        /// <param name="areGood">Whether to generate good or bad files</param>
        /// <param name="validationProfile">The profile that should be used to validate against the NIST online validator. If null or string.Empty, no content validation is performed</param>
        /// <returns></returns>
        public static int GenerateOutputFiles(
            ActionConfiguration actionConfig,
            FileInfo masterFile, 
            DirectoryInfo outputLocation, 
            string dataSource, 
            bool areGood,
            string validationProfile,
            int maxOutput)
        {
            SampleDataSet dataSet = null;
            StreamReader reader = null;
            Dictionary<string, int> activeRows = null;
            Dictionary<string, string[]> activeRowsData = null;
            Instance[] instances = null;
            Dictionary<Instance, List<int>> randomHits = null;

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
                // Try the datasource as an XLS file first.
                FileInfo dataSourceFile = new FileInfo(dataSource);
                dataSet = SampleDataSetRetriever.GetFromXLS(dataSourceFile);
            }
            catch
            {
                try
                {
                    // Now try the datasource as an ODBC connection string. If it fails, write a log entry.
                    dataSet = SampleDataSetRetriever.GetFromOdbc(dataSource);
                }
                catch (Exception e)
                {
                    LogFactory.Log(
                        LogFactory.Severities.Error,
                        LogFactory.MessageTypes.Generation,
                        dataSource,
                        "Error opening data source file: {0}",
                        e.Message);
                    return -1;
                }
            }

            GetOutputFilesCount(actionConfig, dataSet, out numOutputFiles, out largestSection);

            if (maxOutput != -1 && numOutputFiles > maxOutput)
            {
                numOutputFiles = maxOutput;
            }

            randomHits = GenerateRandomHits(actionConfig, numOutputFiles);

            // Open the master file and read it to a string and parse it
            try
            {
                using (reader = new StreamReader(masterFile.FullName))
                {
                    originalContent = reader.ReadToEnd();
                }

                originalDocument.LoadXml(originalContent);

                XmlAttribute schemaLocationAttr = originalDocument.DocumentElement.Attributes["xsi:schemaLocation"];
                if (schemaLocationAttr != null)
                {
                    originalDocument.DocumentElement.Attributes.Remove(schemaLocationAttr);
                }
            }
            catch (Exception e)
            {
                LogFactory.Log(
                    LogFactory.Severities.Error, 
                    LogFactory.MessageTypes.Generation, 
                    masterFile.FullName, 
                    "Error reading and parsing master file: {0}", 
                    e.Message);
            }

            //Generate the output files
            for (int i = 0; i < numOutputFiles; ++i)
            {
                Console.WriteLine("\nWorking on output #{0}\n---------------------------------", i + 1);
                
                try
                {
                    //Get a new copy of the file content
                    currentContent = originalContent;

                    //Retrieve the active sections for this file
                    activeRows = GetActiveSectionRows(dataSet, i, largestSection);
                    activeRowsData = GetActiveSectionRowsData(dataSet, activeRows);

                    instances = GetApplicableInstances(
                        actionConfig, 
                        activeRows, 
                        areGood,
                        randomHits,
                        i+1);

                    if (!areGood && (instances == null || instances.Length == 0))
                    {
                        Console.WriteLine("Nothing to do.");
                        continue;
                    }

                    //If generating good files with no config instances, simply do the tokenization
                    if(areGood && ((instances == null) || (instances.Length == 0)))
                    {
                        //Create the output file
                        outputPath = GetOutputFileName(outputLocation, i, masterFile);

                        //Tokenize and write the resulting string
                        currentContent = TokenizeString(dataSet, activeRowsData, currentContent);

                        // Load in XmlDocument to save so that it is pretty-print'd
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(currentContent);
                        doc.Save(outputPath);

                        ValidationHelper.ValidateDocumentSchema(outputPath);
                        ++numOutputFilesWritten;
                    }
                    else
                    {
                        tempDocument = null;

                        //Write one file per instance, per row
                        foreach (Instance instance in instances)
                        {
                            if (tempDocument == null || instance.createNew)
                            {
                                if (tempDocument != null && !string.IsNullOrEmpty(outputPath))
                                {
                                    // Always validate against the schema
                                    ValidationHelper.ValidateDocumentSchema(outputPath);

                                    if (!string.IsNullOrEmpty(validationProfile))
                                    {
                                        ValidationHelper.ValidateDocumentContent(outputPath, validationProfile);
                                    }
                                }

                                tempDocument = (XmlDocument)originalDocument.Clone();

                                //Create the output file
                                outputPath = GetOutputFileName(outputLocation, i, masterFile, instance);
                            }

                            using (LogFactoryContext logContext = new LogFactoryContext(outputPath))
                            {
                                ActionPerformer performer = new ActionPerformer(tempDocument, actionConfig.namespaces, instance);
                                performer.Execute();
                            }

                            currentContent = tempDocument.OuterXml;

                            //Tokenize and write the resulting string
                            currentContent = TokenizeString(dataSet, activeRowsData, currentContent);

                            // Load in XmlDocument to save so that it is pretty-print'd
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(currentContent);
                            doc.Save(outputPath);
                            ++numOutputFilesWritten;
                        }

                        if (outputPath != null)
                        {
                            // Always validate against the schema
                            ValidationHelper.ValidateDocumentSchema(outputPath);

                            if (!string.IsNullOrEmpty(validationProfile))
                            {
                                ValidationHelper.ValidateDocumentContent(outputPath, validationProfile);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    LogFactory.Log(
                        LogFactory.Severities.Error, 
                        LogFactory.MessageTypes.Generation, 
                        outputPath, 
                        "Unable to write file '{0}': {1}", 
                        outputPath, 
                        e);
                }
            }

            return numOutputFilesWritten;
        }

        internal static Dictionary<Instance, List<int>> GenerateRandomHits(ActionConfiguration config, int numOutputFiles)
        {
            Dictionary<Instance, List<int>> randomHits = new Dictionary<Instance, List<int>>();
            List<Instance> instances = new List<Instance>();

            if (config != null)
            {
                if (config.GoodInstances != null)
                {
                    instances.AddRange(config.GoodInstances);
                }

                if (config.BadInstances != null)
                {
                    instances.AddRange(config.BadInstances);
                }
            }

            List<Instance> randomInstances = instances.Where(y => y.filter != null && y.filter.Item is InstanceFilterRandomRowCount).ToList();

            foreach (Instance cInstance in randomInstances.Take(randomInstances.Count > numOutputFiles ? numOutputFiles : randomInstances.Count))
            {
                InstanceFilterRandomRowCount randomRowCount = cInstance.filter != null ? cInstance.filter.Item as InstanceFilterRandomRowCount : null;

                if (randomRowCount != null)
                {
                    List<int> hits = new List<int>();

                    for (int i = 0; i < randomRowCount.maxOccur; i++)
                    {
                        int randValue = new Random().Next(1, numOutputFiles);

                        // Make sure the hit is different from the last
                        while (hits.Contains(randValue) || HitUsedInException(randomHits, randValue))
                        {
                            randValue = new Random().Next(1, numOutputFiles+1);
                        }

                        if (!hits.Contains(randValue))
                        {
                            hits.Add(randValue);
                        }
                    }

                    randomHits.Add(cInstance, hits);
                }
            }

            return randomHits;
        }

        private static bool HitUsedInException(Dictionary<Instance, List<int>> randomHits, int hit)
        {
            foreach (List<int> cHits in randomHits.Values)
            {
                if (cHits.Contains(hit))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Pulls the correct rows from the sections for use in tokenization
        /// </summary>
        /// <param name="dataSet">DataSet containing the sections</param>
        /// <param name="index">The current index, this is the row in the largest section, mod in the smaller</param>
        /// <param name="largestSectionName">The name of the largest section</param>
        /// <returns>A dictionary containing the appropriate data rows from each section</returns>
        public static Dictionary<string, int> GetActiveSectionRows(SampleDataSet dataSet, int index, string largestSectionName)
        {
            Dictionary<string, int> ret = new Dictionary<string, int>();
            
            foreach (string sectionName in dataSet.Sections.Keys)
            {
                if (dataSet.Sections[sectionName].Data.Length == 0)
                    continue;

                ret.Add(sectionName, (index % dataSet.Sections[sectionName].Data.Length));
            }

            return ret;
        }

        /// <summary>
        /// Convenience method. Pulls the data for the correct rows from the sections for use in tokenization.
        /// </summary>
        /// <param name="dataSet">DataSet containing the sections</param>
        /// <param name="activeSectionRows">The array retreived from GetActiveSectionRows</param>
        /// <returns>A dictionary containing the appropriate data row valuess from each section</returns>
        public static Dictionary<string, string[]> GetActiveSectionRowsData(SampleDataSet dataSet, Dictionary<string, int> activeSectionRows)
        {
            Dictionary<string, string[]> ret = new Dictionary<string, string[]>();

            foreach (string sectionName in activeSectionRows.Keys)
            {
                ret.Add(sectionName, dataSet.Sections[sectionName].Data[activeSectionRows[sectionName]]);
            }

            return ret;

        }

        /// <summary>
        /// Generates the output file name to write to
        /// </summary>
        /// <param name="outputLocation">Output directory</param>
        /// <param name="index">Index of the file</param>
        /// <param name="masterFile">Main input file, us used as a name</param>
        /// <param name="instance">The instance to pull a name from</param>
        /// <returns></returns>
        public static string GetOutputFileName(DirectoryInfo outputLocation, int index, FileInfo masterFile, Instance instance)
        {
            string fileName = "";

            if (instance != null && instance.createNew && !String.IsNullOrEmpty(instance.name))
            {
                fileName = instance.name + "_" + masterFile.Name.Substring(0, masterFile.Name.IndexOf(masterFile.Extension)) +
                    "_" + (index + 1) + masterFile.Extension;
            }
            else
            {
                fileName = masterFile.Name.Substring(0, masterFile.Name.IndexOf(masterFile.Extension)) +
                    "_" + (index + 1) + masterFile.Extension;
            }



#if DEBUG
            System.Console.WriteLine("Writing to file: " + fileName);
#endif

            return outputLocation.FullName + "\\" + fileName;
        }

        /// <summary>
        /// Generates the output file name to write to
        /// </summary>
        /// <param name="outputLocation">Output directory</param>
        /// <param name="index">Index of the file</param>
        /// <param name="masterFile">Main input file, us used as a name</param>
        /// <returns></returns>
        public static string GetOutputFileName(DirectoryInfo outputLocation, int index, FileInfo masterFile)
        {
            return GetOutputFileName(outputLocation, index, masterFile, null);
        }


        /// <summary>
        /// Returns the number of output files that should be written
        /// </summary>
        /// <param name="dataSet">Dataset to calculate based from</param>
        /// <param name="fileCount">The number of files to generate</param>
        /// <param name="largestSection">The name of the largest section</param>
        public static void GetOutputFilesCount(ActionConfiguration config, SampleDataSet dataSet, out int fileCount, out string largestSection)
        {
            fileCount = -1;
            largestSection = "";

            Section section = null;
            foreach (string sectionName in dataSet.Sections.Keys)
            {
                section = dataSet.Sections[sectionName];
                if (section.Data.Length > fileCount)
                {
                    fileCount = section.Data.Length;
                    largestSection = section.Name;
                }
            }

            if (config != null)
            {
                // Determine if there are more good instances to process than we have currently decided to create
                try
                {
                    var uniqueGoodInstances =
                        config.GoodInstances.Select(y => y.createNew && !y.isAbstract);

                    if (uniqueGoodInstances.Count() > fileCount)
                    {
                        fileCount = uniqueGoodInstances.Count();
                    }
                }
                catch { }

                // Determine if there are more bad instances to process than we have currently decided to create
                try
                {
                    var uniqueBadInstances =
                        config.BadInstances.Select(y => y.createNew && !y.isAbstract);

                    if (uniqueBadInstances.Count() > fileCount)
                    {
                        fileCount = uniqueBadInstances.Count();
                    }
                }
                catch { }
            }

            if (fileCount == -1)
            {
                LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Unable to calculate number of output files.");
            }
        }

        /// <summary>
        /// Returns an array of Instances that can be applied to a given set of 
        /// rows within a given set of sections
        /// </summary>
        /// <param name="configuration">The ActionConfiguration</param>
        /// <param name="activeSections">The current collections of sections and rows</param>
        /// <param name="useGood">Whether to use the good or bad section</param>
        /// <returns>The set of Instances that applies</returns>
        public static Instance[] GetApplicableInstances(
            ActionConfiguration configuration,
            Dictionary<string, int> activeSections, 
            bool useGood,
            Dictionary<Instance, List<int>> randomHits,
            int currentHit)
        {
            List<Instance> ret = new List<Instance>();
            List<Instance> instances = null;

            // If no configuration is provided, return empty list
            if (configuration != null)
            {
                instances = useGood ? 
                    configuration.GoodInstances.Where(y => !y.isAbstract).ToList() : 
                    configuration.BadInstances.Where(y => !y.isAbstract).ToList();

                // Ensure there are some instances to test
                if (instances != null)
                {
                    // Iterate over all configuration instances
                    foreach (Instance instance in instances)
                    {
                        if (instance.filter == null || CheckFilter(instance, activeSections, randomHits, currentHit))
                        {
                            ret.Add(instance);

                            List<Instance> inheritedInstances = GetInheritedInstances(configuration, instance);

                            // Add any inherited instances
                            foreach (Instance cInstance in inheritedInstances)
                            {
                                if (cInstance.filter == null || CheckFilter(cInstance, activeSections, randomHits, currentHit))
                                {
                                    ret.Add(cInstance);
                                }
                            }
                        }
                    }
                }
            }

            // Return the applicable instances
            return ret.ToArray();
        }

        private static List<Instance> GetInheritedInstances(ActionConfiguration configuration, Instance instance)
        {
            List<Instance> retInstances = new List<Instance>();

            if (instance.inherit != null)
            {
                foreach (InstanceInherit cInherit in instance.inherit)
                {
                    Instance inheritedInstance = configuration.AllInstances.SingleOrDefault(y => y.name == cInherit.instanceName);

                    if (inheritedInstance != null)
                    {
                        inheritedInstance = Helper.Clone<Instance>(inheritedInstance);
                        inheritedInstance.createNew = false;        // Do not allow inherited instances to create new copies.

                        retInstances.Add(inheritedInstance);

                        // Recursively add additional inherited instances.
                        retInstances.AddRange(
                            GetInheritedInstances(configuration, inheritedInstance));
                    }
                }
            }

            return retInstances;
        }

        /// <summary>
        /// Parses the row information for the filter.
        /// </summary>
        /// <param name="rowInfo">The rowInfo value from the filter.</param>
        /// <returns>A list of rows that the rowInfo represents in string format.</returns>
        public static List<int> ParseRowInfo(string rowInfo)
        {
            List<int> ret = new List<int>();
            string[] commaParts = rowInfo.Split(',');

            foreach (string cCommaPart in commaParts)
            {
                string[] rangeParts = cCommaPart.Split('-');

                if (rangeParts.Length == 2)
                {
                    int partOneValue = 0;
                    int partTwoValue = 0;

                    if (!Int32.TryParse(rangeParts[0], out partOneValue))
                    {
                        LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Invalid range value '{0}' for row filter", rangeParts[0]);
                        continue;
                    }

                    if (!Int32.TryParse(rangeParts[1], out partTwoValue))
                    {
                        LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Invalid range value '{1}' for row filter", rangeParts[1]);
                        continue;
                    }

                    int lowValue = partOneValue < partTwoValue ? partOneValue : partTwoValue;
                    int highValue = partTwoValue > partOneValue ? partTwoValue : partOneValue;

                    for (int i = lowValue; i <= highValue; i++)
                    {
                        if (!ret.Contains(i))
                        {
                            ret.Add(i);
                        }
                    }
                }
                else if (rangeParts.Length == 1)
                {
                    int value = 0;

                    if (!Int32.TryParse(rangeParts[0], out value))
                    {
                        LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Invalid row filer value '{0}'", rangeParts[0]);
                        continue;
                    }

                    if (!ret.Contains(value))
                    {
                        ret.Add(value);
                    }
                }
            }

            return ret;
        }

        /// <summary>
        /// Perform a tokenization on the provided body of text using the given data
        /// </summary>
        /// <param name="dataSet">SimpleDataSet containing the tokens</param>
        /// <param name="activeRowsData">Current set of token values</param>
        /// <param name="content">Content to tokenize</param>
        /// <returns>The tokenized string</returns>
        public static string TokenizeString(SampleDataSet dataSet, Dictionary<string, string[]> activeRowsData, 
            string content)
        {
            string value = "";
            Token token = null;

            //Iterate over each token
            foreach (string key in dataSet.Tokens.Keys)
            {
                token = dataSet.Tokens[key];

                //Ensure the section can be found
                if (!activeRowsData.Keys.Contains(token.SectionName))
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Unable to find section " + token.SectionName + " as specified in configuration.");
                    value = DefaultValue;
                }
                //Ensure the value can be found
                else if (activeRowsData[token.SectionName].Length < token.Column)
                {
                    LogFactory.Log(LogFactory.Severities.Error, LogFactory.MessageTypes.Generation, string.Empty, "Unable to find value in column " + token.Column
                        + " in section " + token.SectionName + " as specified in configuration.");
                    value = DefaultValue;
                }
                else
                {
                    //NOTE: Decrement column by one, assuming 1-based counting in config, 0-based in code
                    value = activeRowsData[token.SectionName][token.Column - 1];
                }
                        
                //Replace all tokens
                content = Regex.Replace(content, token.Name, value);
            }

            return content;
        }

        /// <summary>
        /// Determines if the filter excludes the instances or not.
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="activeSections"></param>
        /// <param name="randomHits"></param>
        /// <param name="currentHit"></param>
        /// <returns>True if the filter indicates the instance should be included. False if the filter excludes the instance.</returns>
        private static bool CheckFilter(
            Instance instance,
            Dictionary<string, int> activeSections,
            Dictionary<Instance, List<int>> randomHits,
            int currentHit)
        {
            string section = string.Empty;
            string rowInfo = string.Empty;

            //If it's specific, check to see if that applies to this row
            if (instance.filter.Item is InstanceFilterSpecificRowCount)
            {
                section = (instance.filter.Item as InstanceFilterSpecificRowCount).section;
                rowInfo = (instance.filter.Item as InstanceFilterSpecificRowCount).recordValue;

                List<int> desiredRows = ParseRowInfo(rowInfo);

                if (activeSections.Keys.Contains(section) && (desiredRows.Contains(activeSections[section]+1)))     // Use +1 because rowInfo is 1-based-index
                {
                    return true;
                }
            }
            //If it's random, get a random number
            else if (instance.filter.Item is InstanceFilterRandomRowCount)
            {
                if (randomHits != null && randomHits[instance] != null && randomHits[instance].Contains(currentHit))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
