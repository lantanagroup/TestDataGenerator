using System;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Shared.Logic
{
    public class ConfigurationFileParser
    {
        public const string DataSourcePI = "data-source";
        public const string MasterFilePI = "master-file";

        /// <summary>
        /// Parses a given configuration file and returns the included Token entities
        /// </summary>
        /// <param name="configFile">The input configuration file</param>
        /// <returns>A list of tokens</returns>
        public static ActionConfiguration ParseConfigurationFile(FileInfo configFile, out string dataSource, out string masterFile)
        {
            dataSource =
                masterFile = null;

            if (!File.Exists(configFile.FullName))
            {
                return null;
            }

            XmlDocument doc = new XmlDocument();

            try
            {
                doc.Load(configFile.FullName);

                for (int i = 0; i < doc.DocumentElement.ChildNodes.Count; i++)
                {
                    XmlNode cNode = doc.DocumentElement.ChildNodes[i];
                    if (cNode is XmlEntityReference)
                    {
                        cNode.ParentNode.InsertBefore(cNode.ChildNodes[0].Clone(), cNode);
                        cNode.ParentNode.RemoveChild(cNode);
                    }
                }

                XmlProcessingInstruction dsPI =
                    doc.SelectSingleNode("processing-instruction('" + DataSourcePI + "')") as XmlProcessingInstruction;
                XmlProcessingInstruction mfPI =
                    doc.SelectSingleNode("processing-instruction('" + MasterFilePI + "')") as XmlProcessingInstruction;

                if (dsPI != null)
                {
                    dataSource = dsPI.Value;
                }

                if (mfPI != null)
                {
                    masterFile = mfPI.Value;
                }

                dataSource = Helper.MakeAbsolutePath(configFile.FullName, dataSource);
                masterFile = Helper.MakeAbsolutePath(configFile.FullName, masterFile);
            }
            catch
            {
                return null;
            }

            using (MemoryStream docStream = new MemoryStream())
            {
                doc.Save(docStream);

                docStream.Position = 0;

                XmlSerializer serializer = new XmlSerializer(typeof(ActionConfiguration));
                ActionConfiguration config = serializer.Deserialize(docStream) as ActionConfiguration;

                if (config != null)
                {
                    var instanceNames = (from p in config.AllInstances select p.name);
                    var distinctNames = instanceNames.Distinct();

                    if (distinctNames.Count() != instanceNames.Count())
                    {
                        throw new Exception("The action configuration contains multiple instances with the same name. Instances must have unique names.");
                    }
                }

                return config;
            }
        }
    }
}
