using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace LantanaGroup.TestDataGenerator.Shared.Data
{
    /// <summary>
    /// Represents a collection of multiple sections of data
    /// </summary>
    public class SampleDataSet
    {
        /// <summary>
        /// Constructs an empty SampleDataSet
        /// </summary>
        public SampleDataSet()
        {
            Sections = new Dictionary<string, Section>();
            Tokens = new Dictionary<string, Token>();
        }

        /// <summary>
        /// An individual section of data, categorized by its name
        /// </summary>
        public Dictionary<string, Section> Sections
        { get; set; }

        /// <summary>
        /// All tokens configured with the data, categorized by name
        /// </summary>
        public Dictionary<string, Token> Tokens
        { get; set; }

    }
}
