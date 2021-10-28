using System;
using System.Data.Odbc;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LantanaGroup.TestDataGenerator.Shared
{
    public class Helper
    {
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
            {
                throw new ArgumentException("The type must be serializable.", "source");
            }

            // Don't serialize a null object, simply return the default for that object
            if (Object.ReferenceEquals(source, null))
            {
                return default(T);
            }

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                formatter.Serialize(stream, source);
                stream.Seek(0, SeekOrigin.Begin);
                return (T)formatter.Deserialize(stream);
            }
        }

        /// <summary>
        /// If the inputFile has a relative path, the returned file contains an absolute path which matches path of the matchFile.
        /// matchFile is always expected to be an actual file path, while input may be either a connection string or a file path.
        /// </summary>
        /// <param name="matchFileName">The file that inputFile should be absolute to</param>
        /// <param name="input">The input connection string or file that should be fixed</param>
        /// <returns>A fixed string that has an absolute path for the files within it.</returns>
        public static string MakeAbsolutePath(string matchFileName, string input)
        {
            FileInfo matchFile = new FileInfo(matchFileName);

            try
            {
                FileInfo inputFile = new FileInfo(input);

                if (string.IsNullOrEmpty(Path.GetDirectoryName(input)))
                {
                    return Path.Combine(matchFile.Directory.FullName, input);
                }
            }
            catch { }

            try
            {
                OdbcConnectionStringBuilder csb = new OdbcConnectionStringBuilder(input);

                object dbq = null;
                if (csb.TryGetValue("Dbq", out dbq))
                {
                    string dbqValue = dbq as string;

                    if (!string.IsNullOrEmpty(dbqValue) && string.IsNullOrEmpty(Path.GetDirectoryName(dbqValue)))
                    {
                        string newDbq = Path.Combine(matchFile.Directory.FullName, dbqValue);

                        csb.Remove("Dbq");
                        csb.Add("dbq", newDbq);
                    }
                }

                return csb.ToString();
            }
            catch { }

            return input;
        }

        /// <summary>
        /// If the path in the input matches the path of the matchFileName, then the 
        /// input's name is returned alone (as a relative path to the matchFileName).
        /// If input is an ODBC connection string, the Dbq key is searched for and checked.
        /// </summary>
        /// <param name="matchFileName">The file that inputFile should be absolute to</param>
        /// <param name="input">The input connection string or file that should be fixed</param>
        /// <returns>A fixed version of the input parameter that has a relative path, if possible.</returns>
        public static string MakeRelativePath(string matchFileName, string input)
        {
            FileInfo matchFile = new FileInfo(matchFileName);

            try
            {
                FileInfo inputFile = new FileInfo(input);

                if (inputFile.Directory.Name == matchFile.Directory.Name)
                {
                    return inputFile.Name;
                }
            }
            catch { }

            try
            {
                OdbcConnectionStringBuilder csb = new OdbcConnectionStringBuilder(input);

                object dbq = null;
                if (csb.TryGetValue("Dbq", out dbq))
                {
                    string dbqValue = dbq as string;

                    if (!string.IsNullOrEmpty(dbqValue))
                    {
                        FileInfo dbqFile = new FileInfo(dbqValue);

                        if (dbqFile.Directory.Name == matchFile.Directory.Name)
                        {
                            csb.Remove("Dbq");
                            csb.Add("dbq", dbqFile.Name);
                        }
                    }
                }

                return csb.ToString();
            }
            catch { }

            return input;
        }
    }
}
