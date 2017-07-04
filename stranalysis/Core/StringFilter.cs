using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stranalysis.Core
{
    /// <summary>
    /// A static class for parsing a string line
    /// </summary>
    public static class StringFilter
    {
        /// <summary>
        /// Parses the string by running it through several filters
        /// </summary>
        public static string Parse(string data)
        {
            if (!FilterWhitespace(data)) return null;
            if (!FilterGeneralJunk(data)) return null;

            return data;
        }

        /// <summary>
        /// Filter 1: 
        /// Check if the string is empty or white spaced
        /// </summary>
        private static bool FilterWhitespace(string data)
        {
            return !string.IsNullOrWhiteSpace(data);
        }

        /// <summary>
        /// Filter 2:
        /// Check for the 3 character junk format:
        /// [CHAR_A][CHAR_B][CHAR_A]
        /// </summary>
        private static bool FilterGeneralJunk(string data)
        {
            if (data.Length != 3) return true;

            if ((data[0] == data[1] && data[2] != data[0]) ||
                (data[0] == data[2] && data[1] != data[0]) ||
                (data[1] == data[2] && data[0] != data[1]))
                return false;

            return true;
        }
        
    }
}
