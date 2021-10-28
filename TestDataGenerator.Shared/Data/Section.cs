using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LantanaGroup.TestDataGenerator.Shared.Data
{
    /// <summary>
    /// Represents a tabular section of data, either a table in a database 
    /// or an XLS worksheet
    /// </summary>
    public class Section
    {

        /// <summary>
        /// Initialize the Section
        /// </summary>
        /// <param name="name"></param>
        public Section(string name)
        { Name = name; }

        /// <summary>
        /// The name of this section, i.e. "Header" or "Results"
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// The collection of data within this section, in a tabular-format
        /// for easy access.
        /// </summary>
        public string[][] Data
        { get; set; }
    }
}
