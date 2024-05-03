using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Collections;
using System.Data;
using System.Data.Odbc;
using System.Data.OleDb;
using LantanaGroup.TestDataGenerator.Shared.Data;

namespace LantanaGroup.TestDataGenerator.Shared.Logic
{
    public class SampleDataSetRetriever
    {
        private const string OleDbXlsConnectionStringFormat = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};Extended Properties=Excel 8.0";
        private const string OdbcXlsConnectionStringFormat = "Driver={{Microsoft Excel Driver (*.xls)}};DriverId=790;Dbq={0}";
        private const string ConfigurationSheetName = "Configuration";

        /// <summary>
        /// Parses an input file containing multiple worksheets. Each sheet
        /// is returned as a separate Section within the DataSet, with
        /// the data from each sheet inserted in the Section's
        /// Data array.
        /// </summary>
        /// <param name="inputFile">The file to parse</param>
        /// <returns></returns>
        public static SampleDataSet GetFromXLS(FileInfo inputFile)
        {
            string connectionString = string.Format(OdbcXlsConnectionStringFormat, inputFile.FullName);

            if (!inputFile.Exists)
                throw new FileNotFoundException("Input file not found!", inputFile.FullName);

            // TODO: See what happens with duplicate sheet names
            // verify names are accessible and not modified
            // possibly throw error if duplicate sheet names are identified

            return GetFromOdbc(connectionString);
        }

        public static SampleDataSet GetFromOdbc(string connectionString)
        {
            SampleDataSet dataSet = new SampleDataSet();

            using (OdbcConnection connection = new OdbcConnection(connectionString))
            {
                string query = "SELECT * FROM {0} ";
                ArrayList tempList = null;
                DataTable tables = null;
                bool configFound = false;

                connection.Open();                
                tables = connection.GetSchema("tables");

                foreach (DataRow row in tables.Rows)
                {
                    string tableName = row["TABLE_NAME"].ToString();
                    if (String.IsNullOrEmpty(tableName) || tableName.EndsWith("_"))
                        continue;

                    Section section = new Section(tableName);
                    tempList = new ArrayList();

                    //Define the query, using square brackets due to $ char in worksheet name
                    query = "SELECT * FROM [" + tableName + "]";
                    OdbcCommand command = connection.CreateCommand();
                    command.CommandText = query;
                    OdbcDataReader reader = command.ExecuteReader();

                    if ((tableName.Substring(0, tableName.Length - 1)).ToUpper().Equals(ConfigurationSheetName.ToUpper()))
                    {
                        configFound = true;
                        // TODO: Support the use of column names in the configuration instead of just numbers
                        dataSet.Tokens = HandleConfigurationTab(reader);
                    }

                    else
                    {
                        //Add the section, losing the last char as it is $
                        section = HandleSection(reader, tableName.Substring(0, tableName.Length - 1));
                        dataSet.Sections.Add(section.Name, section);
                    }
                }

                if (!configFound)
                {
                    throw new Exception("No " + ConfigurationSheetName + " sheet found in the input XLS!");
                }
            }

            return dataSet;
        }

        /// <summary>
        /// Handles data in any XLS tab, creating a Section to encapsulate it
        /// </summary>
        /// <param name="reader">A reader holding the results of a configuration tab</param>
        /// <param name="sectionName">The name of the section</param>
        /// <returns>A fully-populated section</returns>
        private static Section HandleSection(OdbcDataReader reader, string sectionName)
        {
            string[] values = null;
            ArrayList tempList = new ArrayList();
            Section ret = new Section(sectionName);

            //Iterate over results
            for (int i = 0; reader.Read(); ++i)
            {
                //Assign values to temp array for now
                values = new string[reader.FieldCount];
                for (int j = 0; j < reader.FieldCount; ++j)
                {
                    if (reader[j] != null && reader[j].ToString().Trim() != string.Empty)
                        values[j] = reader[j].ToString();
                    else
                        values[j] = "";
                }

                //Store values in temp List for now
                tempList.Add(values);
            }

            //TODO: Find more efficient way to do this

            //Copy data over to the section
            List<string[]> dataList = new List<string[]>();
            for (int i = 0; i < tempList.Count; ++i)
            {
                // Determine if the row is just a bunch of empty values
                string rowValue = string.Empty;
                for (int x = 0; x < ((string[])tempList[i]).Length; x++)
                {
                    rowValue += ((string[])tempList[i])[x];
                }

                // If not an empty-value'd row, add it to the list
                if (rowValue.Trim() != string.Empty)
                {
                    dataList.Add((string[])tempList[i]);
                }
            }

            ret.Data = dataList.ToArray();

            return ret;
        }

        /// <summary>
        /// Parses a given Configuration tab in an XLS
        /// </summary>
        /// <param name="reader">A reader holding the results of a configuration tab</param>
        /// <returns>A Dictionary populated with Tokens</returns>
        private static Dictionary<string, Token> HandleConfigurationTab(OdbcDataReader reader)
        {
            Token tempToken = null;
            Dictionary<string, Token> ret = new Dictionary<string, Token>();
            int row = 0;

            while (reader.Read())
            {
                row++;

                String token = reader[0] != null ? reader[0].ToString() : null;
                String sheet = reader[1] != null ? reader[1].ToString() : null;
                String col = reader[2] != null ? reader[2].ToString() : null;

                if (!String.IsNullOrEmpty(token) && !String.IsNullOrEmpty(sheet) && !String.IsNullOrEmpty(col))
                {
                    try
                    {
                        tempToken = new Token(token, sheet, int.Parse(col));
                        ret.Add(tempToken.Name, tempToken);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Can't parse row " + row + " of Configuration tab: " + ex.Message);
                        continue;
                    }
                }
                else
                {
                    Console.WriteLine("Skipping row " + row + ". Row not completed.");
                    continue;
                }
            }

            return ret;
        }
    }
}
