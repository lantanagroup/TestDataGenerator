using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LantanaGroup.TestDataGenerator.Shared
{
    public class LogException : Exception
    {
        public LogException(string message)
            : base(message)
        {

        }
    }
}
