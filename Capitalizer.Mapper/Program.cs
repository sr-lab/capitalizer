using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Capitalizer.Shared;

namespace Capitalizer.Mapper
{
    class Program
    {
        /// <summary>
        /// Strips all non-letter characters from a string and returns the result.
        /// </summary>
        /// <param name="str">The string to strip non-letters from.</param>
        /// <returns></returns>
        private static string StripNonLetters(string str)
        {
            var filtered = "";
            for (var i = 0; i < str.Length; i++)
            {
                // Only append if next character is letter.
                if (char.IsLetter(str[i]))
                {
                    filtered += str[i];
                }
            }
            return filtered;
        }

        static void Main(string[] args)
        {
            // Check file exists.
            if (!File.Exists(args[0]))
            {
                Console.WriteLine("Could not read input file.");
                return;
            }

            // Read in entire file.
            var lines = FileUtils.ReadFileAsLines(args[0]);

            // Should we go from the end instead?
            var fromEnd = args.Contains("-r");
            var stripNonLetters = args.Contains("-s");

            // Build an uppercase letter 'heatmap' by index of occurence.
            var dict = new SortedDictionary<int, int>();
            foreach (var line in lines)
            {
                // Strip all non-letters from string, if specified.
                var stripped = stripNonLetters ? StripNonLetters(line) : line;
                for (int j = 0; j < stripped.Length; j++)
                {
                    var k = fromEnd ? stripped.Length - 1 - j : j;
                    if (char.IsUpper(stripped[k]))
                    {
                        // Increment entry in capital letter heatmap.
                        if (!dict.ContainsKey(j))
                        {
                            dict.Add(j, 0);
                        }
                        dict[j]++;
                    }
                }
            }

            // Print out total.
            var totalCount = lines.Count(x => x.Any(y => char.IsUpper(y)));
            Console.WriteLine($"A total of {totalCount}/{lines.Count()} contain uppercase" +
                $" characters ({(totalCount/lines.Count())*100}%).");

            // Add keys (indices) to list.
            var indices = new List<int>();
            foreach (var entry in dict)
            {
                indices.Add(entry.Key);
            }

            // Sort indices.
            var keys = indices.ToArray();
            Array.Sort(keys);

            // Print out indices in order with corresponding number of occurences.
            for (int i = 0; i < keys.Length; i++)
            {
                Console.WriteLine($"{keys[i]}:{dict[keys[i]]}");
            }
        }
    }
}
