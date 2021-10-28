using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LantanaGroup.TestDataGenerator.Shared.Data
{

    /// <summary>
    /// Represents a configuration for a given token
    /// </summary>
    public class Token
    {
        /// <summary>
        /// Initializes a Token instance
        /// </summary>
        /// <param name="name">The name of a given token, i.e. %result_id%</param>
        /// <param name="sectionName">The name of the SampleDataSet section to look within to find the value of this token, i.e. "Results"</param>
        /// <param name="column">The column within the Section to look within</param>
        public Token(string name, string sectionName, int column)
        {
            Name = name;
            SectionName = sectionName;
            Column = column;
        }

        /// <summary>
        /// The name of a given token, i.e. %result_id%
        /// </summary>
        public string Name
        { get; set; }

        /// <summary>
        /// The name of the SampleDataSet section to look within to find the 
        /// value of this token, i.e. "Results"
        /// </summary>
        public string SectionName
        { get; set; }

        /// <summary>
        /// The column within the Section to look within
        /// </summary>
        public int Column
        { get; set; }
    }
}
