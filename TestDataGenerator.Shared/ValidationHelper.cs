using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using System.IO;

namespace LantanaGroup.TestDataGenerator.Shared
{
    public class ValidationHelper
    {
        private static List<string> ignoredValidationTestErrors = new List<string>() {
            { "/cda:ClinicalDocument/cda:recordTarget/cda:patientRole//cda:id[@root=cda:participantRole/cda:id/@root]" },
            { "document('http://localhost:8080/hitspValidation/schematron/ccd/voc.xml')/systems/system[@codeSystemName='SNOMED CT'][@group='AlertStatusCode']/code[@value = current()/cda:value/@code]" }
        };

        /// <summary>
        /// Performs a validating parse of the XML file at the provided path.
        /// Throws an exception if validation fails. Optionally validates
        /// against CDA.xsd
        /// </summary>
        /// <param name="outputPath">Path to the XML file</param>
        /// <returns>True if passes, exception if not</returns>
        public static bool ValidateDocumentBasic(string outputPath)
        {
            XmlDocument tempDocument = null;

            try
            {
                //Validate basic XML well-formedness
                tempDocument = new XmlDocument();
                tempDocument.Load(outputPath);
            }
            catch (Exception e)
            {
                throw new Exception("Unable to parse file: " + e);
            }

            return true;
        }

        /// <summary>
        /// Performs a validating parse of the XML file at the provided path.
        /// Throws an exception if validation fails. Additionally, validates
        /// against CDA.xsd
        /// </summary>
        /// <param name="outputPath">Path to the XML file</param>
        /// <param name="useSchema">Whether to perform a full schema validation</param>
        /// <returns>True if passes, exception if not</returns>
        public static bool ValidateDocumentSchema(string outputPath)
        {
            if (string.IsNullOrEmpty(Properties.Settings.Default.CDASchemaLocation) || !File.Exists(Properties.Settings.Default.CDASchemaLocation))
            {
                throw new Exception("CDA Schema at \"" + Properties.Settings.Default.CDASchemaLocation + "\" is not defined or is not a valid location. Please check the settings file.");
            }

            try
            {
                XmlReaderSettings settings = new XmlReaderSettings();
                int errorCount = 0;
                settings.CloseInput = true;
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags =
                    XmlSchemaValidationFlags.ReportValidationWarnings |
                    XmlSchemaValidationFlags.ProcessIdentityConstraints |
                    XmlSchemaValidationFlags.ProcessInlineSchema |
                    XmlSchemaValidationFlags.ProcessSchemaLocation;
                settings.ValidationEventHandler += new ValidationEventHandler(
                    delegate(object sender, ValidationEventArgs e)
                    {
                        LogFactory.Severities severity = LogFactory.Severities.Info;

                        switch (e.Severity)
                        {
                            case XmlSeverityType.Error:
                                severity = LogFactory.Severities.Error;
                                break;
                            case XmlSeverityType.Warning:
                                severity = LogFactory.Severities.Warning;
                                break;
                        }

                        LogFactory.Log(
                            severity,
                            outputPath,
                            e.Exception.LineNumber,
                            e.Exception.LinePosition,
                            e.Message.Replace("{", "[").Replace("}", "]"));
                        errorCount++;
                    });
                settings.Schemas.Add("urn:hl7-org:v3", Properties.Settings.Default.CDASchemaLocation);

                Console.WriteLine("Starting validation against schema for '{0}'", outputPath);

                // Perform the validation
                XmlReader reader = XmlReader.Create(new StreamReader(outputPath), settings);
                while (reader.Read())
                {
                    // Do nothing. Validation handler (delegate) does all the work
                }

                LogFactory.Log(LogFactory.Severities.Info, LogFactory.MessageTypes.SchemaValidation, outputPath, "Completed validating against schema ({0} errors)", errorCount);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return true;
        }

        public static bool ValidateDocumentContent(string outputPath, string validationProfile)
        {
            using (NistRemoteValidator.ValidationWebServicePortTypeClient client = new NistRemoteValidator.ValidationWebServicePortTypeClient(Properties.Settings.Default.NistRemoteValidatorEndpoint))
            {
                using (StreamReader reader = new StreamReader(outputPath))
                {
                    LogFactory.Log(LogFactory.Severities.Info, LogFactory.MessageTypes.ContentValidation, outputPath, "Validating content");

                    string content = reader.ReadToEnd();
                    NistRemoteValidator.WSValidationResults results = client.validateDocument(validationProfile, content);

                    int messageCount = 0;

                    if (results.issue != null && results.issue.Length > 0)
                    {
                        foreach (NistRemoteValidator.WSIndividualValidationResult cIssue in results.issue)
                        {
                            if (ignoredValidationTestErrors.Contains(cIssue.test))
                            {
                                continue;
                            }

                            if (cIssue.severity != "warning" || Properties.Settings.Default.ContentValidationIncludesWarnings)
                            {
                                LogFactory.Severities severity = LogFactory.Severities.Info;

                                if (cIssue.severity == "warning")
                                {
                                    severity = LogFactory.Severities.Warning;
                                }
                                else if (cIssue.severity == "error")
                                {
                                    severity = LogFactory.Severities.Error;
                                }

                                LogFactory.Log(severity, outputPath, cIssue.context, cIssue.test, cIssue.message.Replace("{", "[").Replace("}", "]"));
                                messageCount++;
                            }
                        }
                    }

                    LogFactory.Log(LogFactory.Severities.Info, LogFactory.MessageTypes.ContentValidation, outputPath, "Done validating content ({0} messages)", messageCount);
                }
            }

            return false;
        }
    }
}
