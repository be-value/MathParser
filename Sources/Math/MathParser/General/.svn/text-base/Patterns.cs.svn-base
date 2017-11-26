// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Patterns.cs" company="Decerno">
//   (c) 2011, Jelle de Vries
// </copyright>
// --------------------------------------------------------------------------------------------------------------------
namespace MathParser
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the regular expression patterns used by this mathematical framework
    /// </summary>
    internal static class Patterns
    {
        /// <summary>
        /// The list of internally used regular expression patterns
        /// </summary>
        private static readonly List<string> PatternList = InitializePatterns();

        /// <summary>
        /// Gets All regular expression patterns concatenated with logical OR
        /// </summary>
        public static string All
        {
            get
            {
                string output = null;
                for (var i = 0; i < PatternList.Count; i++)
                {
                    if (i == 0)
                    {
                        output += PatternList[i];
                    }
                    else
                    {
                        output += "|" + PatternList[i];
                    }
                }

                return output;
            }
        }

        /// <summary>
        /// Gets the pattern representing the Number lexeme.
        /// </summary>
        public static string Number
        {
            get
            {
                return PatternList[1];
            }
        }

        /// <summary>
        /// Gets the pattern representing the Identifier lexeme.
        /// </summary>
        public static string Identifier
        {
            get
            {
                return PatternList[2];
            }
        }

        /// <summary>
        /// Initializes the static list of patterns used in the mathematical framework
        /// </summary>
        /// <returns>The list of regular expression patterns.</returns>
        private static List<string> InitializePatterns()
        {
            var output = new List<string>
                {
                    @"(\s+)",
                    @"(\d+\.?\d*)",
                    @"([_A-Za-z][_A-Za-z0-9]*)",
                    @"(\-)",
                    @"(\+)",
                    @"(\/)",
                    @"(\*)",
                    @"(\()",
                    @"(\))",
                    @"(\,)"
                };
            return output;
        }
    }
}
