using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace LantanaGroup.TestDataGenerator.Shared
{
    public static class LogFactory
    {
        internal static XmlDocument errorLogDoc = null;
        internal static XmlNode errorLogRoot = null;

        static LogFactory()
        {
            InitializeLog();
        }

        public static void InitializeLog()
        {
            errorLogDoc = new XmlDocument();
            errorLogRoot = errorLogDoc.CreateElement("root");
            errorLogDoc.AppendChild(errorLogRoot);
        }

        public enum Severities 
        {
            Error,
            Warning,
            Info
        }

        public enum MessageTypes
        {
            Generation,
            SchemaValidation,
            ContentValidation
        }

        /// <summary>
        /// Saves the XML log document to the specific path. Additional, savees the MessageLog.xslt to the 
        /// same directory that the log should be saved to and modifies the log XML so that it will use the MessageLog.xslt
        /// when opened in a browser.
        /// </summary>
        /// <param name="outputPath">The destination of the log file.</param>
        public static void SaveDocLog(string outputPath)
        {
            XmlAttribute dateAttr = errorLogDoc.CreateAttribute("date");
            dateAttr.Value = DateTime.Now.ToShortDateString();
            errorLogRoot.Attributes.Append(dateAttr);

            try
            {
                string content = string.Empty;

                using (MemoryStream ms = new MemoryStream())
                {
                    errorLogDoc.Save(ms);

                    ms.Position = 0;

                    StreamReader reader = new StreamReader(ms);
                    content = reader.ReadToEnd();
                }

                using (StreamWriter sw = new StreamWriter(outputPath))
                {
                    sw.Write(
                        content.Insert(0, "<?xml-stylesheet type=\"text/xsl\" href=\"MessageLog.xslt\"?>\n"));
                }

                string outputDirectory = Directory.GetParent(outputPath).FullName;

                using (StreamReader transformInputStream = new StreamReader(
                    System.Reflection.Assembly.GetExecutingAssembly().GetManifestResourceStream("LantanaGroup.TestDataGenerator.MessageLog.xslt")))
                {
                    using (StreamWriter transformOutputStream = new StreamWriter(
                        Path.Combine(outputDirectory, "MessageLog.xslt")))
                    {
                        transformOutputStream.Write(transformInputStream.ReadToEnd());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Could not save to error log file '{0}' due to error: {1}", outputPath, ex.Message);
            }
        }

        /// <summary>
        /// Log a basic message.
        /// </summary>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="type">The type of message.</param>
        /// <param name="file">The file that the message is in relation to.</param>
        /// <param name="addToDoc">True to add the message to the XML output doc, otherwise false.</param>
        /// <param name="messageFormat">The message, or format of the message to output to Console and/or XML log. The format is used in combination with formatArgs.</param>
        /// <param name="formatArgs">The arguments to format the messageFormat with.</param>
        public static void Log(Severities severity, MessageTypes type, string file, bool addToDoc, string messageFormat, params object[] formatArgs)
        {
            string message = string.Format(messageFormat, formatArgs);

            if (severity == Severities.Error)
            {
                Console.Error.WriteLine("ERROR ({0}): {1}", type, message);
            }
            else if (severity == Severities.Warning)
            {
                Console.WriteLine("WARNING ({0}): {1}", type, message);
            }
            else
            {
                Console.WriteLine("{0}: {1}", type, message);
            }

            if (addToDoc)
            {
                AddMessageToDoc(severity, type, file, message);
            }
        }

        /// <summary>
        /// Log a basic message. Always adds message to the XML log output.
        /// </summary>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="type">The type of message.</param>
        /// <param name="file">The file that the message is in relation to.</param>
        /// <param name="messageFormat">The message, or format of the message to output to Console and/or XML log. The format is used in combination with formatArgs.</param>
        /// <param name="formatArgs">The arguments to format the messageFormat with.</param>
        public static void Log(Severities severity, MessageTypes type, string file, string messageFormat, params object[] formatArgs)
        {
            Log(severity, type, file, true, messageFormat, formatArgs);
        }

        /// <summary>
        /// Log a Schema Validation message.
        /// </summary>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="file">The file that the message is in relation to.</param>
        /// <param name="line">The line number that the schema validation error occurred on.</param>
        /// <param name="position">The position on the line that the schema validation error occurred on.</param>
        /// <param name="message">The message to log.</param>
        public static void Log(Severities severity, string file, int line, int position, string message)
        {
            Log(severity, MessageTypes.SchemaValidation, file, false, message);

            AddMessageToDoc(severity, file, line, position, message);
        }

        /// <summary>
        /// Log a Content validation message.
        /// </summary>
        /// <param name="severity">The severity of the message.</param>
        /// <param name="file">The file that the message is in relation to.</param>
        /// <param name="context">The context of the test that resulted in a message.</param>
        /// <param name="test">The test that resulted in a message.</param>
        /// <param name="message">The message to log.</param>
        public static void Log(Severities severity, string file, string context, string test, string message)
        {
            Log(severity, MessageTypes.ContentValidation, file, false, message);

            AddMessageToDoc(severity, file, message, context, test);
        }

        private static XmlElement AddMessageToDoc(Severities severity, MessageTypes type, string file, string message)
        {
            XmlElement messageNode = errorLogDoc.CreateElement("message");

            XmlAttribute severityAttr = errorLogDoc.CreateAttribute("severity");
            messageNode.Attributes.Append(severityAttr);

            switch (severity)
            {
                case Severities.Error:
                    severityAttr.Value = "Error";
                    break;
                case Severities.Warning:
                    severityAttr.Value = "Warning";
                    break;
                case Severities.Info:
                    severityAttr.Value = "Information";
                    break;
            }

            XmlAttribute typeAttr = errorLogDoc.CreateAttribute("type");
            messageNode.Attributes.Append(typeAttr);

            switch (type)
            {
                case MessageTypes.Generation:
                    typeAttr.Value = "Generation";
                    break;
                case MessageTypes.SchemaValidation:
                    typeAttr.Value = "Schema Validation";
                    break;
                case MessageTypes.ContentValidation:
                    typeAttr.Value = "Content Validation";
                    break;
            }

            XmlAttribute timeAttr = errorLogDoc.CreateAttribute("time");
            timeAttr.Value = DateTime.Now.ToString("HH:mm:ss");
            messageNode.Attributes.Append(timeAttr);

            if (!string.IsNullOrEmpty(file))
            {
                XmlAttribute fileAttr = errorLogDoc.CreateAttribute("file");
                fileAttr.Value = file;
                messageNode.Attributes.Append(fileAttr);
            }
            else if (!string.IsNullOrEmpty(LogFactoryContext.FileName))
            {
                XmlAttribute fileAttr = errorLogDoc.CreateAttribute("file");
                fileAttr.Value = LogFactoryContext.FileName;
                messageNode.Attributes.Append(fileAttr);
            }

            XmlAttribute messageAttr = errorLogDoc.CreateAttribute("message");
            messageAttr.Value = message;
            messageNode.Attributes.Append(messageAttr);

            errorLogRoot.AppendChild(messageNode);

            return messageNode;
        }

        private static void AddMessageToDoc(Severities severity, string file, string message, string context, string test)
        {
            XmlElement messageNode = AddMessageToDoc(severity, MessageTypes.ContentValidation, file, message);

            // Context element within <message>
            XmlElement contextNode = errorLogDoc.CreateElement("context");
            contextNode.InnerText = context;
            messageNode.AppendChild(contextNode);

            // Test element within <message>
            XmlElement testNode = errorLogDoc.CreateElement("test");
            testNode.InnerText = test;
            messageNode.AppendChild(testNode);
        }

        private static void AddMessageToDoc(Severities severity, string file, int line, int position, string message)
        {
            XmlElement messageNode = AddMessageToDoc(severity, MessageTypes.SchemaValidation, file, message);

            // Location element within <message>
            XmlElement locationNode = errorLogDoc.CreateElement("location");
            messageNode.AppendChild(locationNode);

            // Line attribute
            XmlAttribute lineAttr = errorLogDoc.CreateAttribute("line");
            lineAttr.Value = line.ToString();
            locationNode.Attributes.Append(lineAttr);

            // Position attribute
            XmlAttribute positionAttr = errorLogDoc.CreateAttribute("position");
            positionAttr.Value = position.ToString();
            locationNode.Attributes.Append(positionAttr);
        }
    }

    public class LogFactoryContext : IDisposable
    {
        private static string fileName = string.Empty;

        public static string FileName
        {
            get { return LogFactoryContext.fileName; }
        }

        public LogFactoryContext(string fileName)
        {
            LogFactoryContext.fileName = fileName;
        }

        #region IDisposable Members

        public void Dispose()
        {
            LogFactoryContext.fileName = string.Empty;
        }

        #endregion
    }
}
