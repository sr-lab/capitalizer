using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Capitalizer.Shared;

namespace Capitalizer.SymbolMapper
{
    class Program
    {
        /// <summary>
        /// Returns true if a character is neither a letter nor a digit.
        /// </summary>
        /// <param name="chr">The character to check.</param>
        /// <returns></returns>
        private static bool IsOther(char chr)
        {
            return !(char.IsDigit(chr) || char.IsLower(chr) || char.IsUpper(chr));
        }

        static void Main(string[] args)
        {
            // Print usage.
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Capitalizer.SymbolMapper <input_file> [-r]");
                Console.WriteLine("[-r] Optional (default off). Map from end instead of beginning.");
                return;
            }

            // Check file exists.
            var path = args[0];
            if (!File.Exists(path))
            {
                Console.WriteLine($"Could not read input file '{path}'.");
                return;
            }

            // Read in entire file.
            var lines = FileUtils.ReadFileAsLines(path);

            // Should we go from the end instead?
            var fromEnd = args.Length > 1 && args[1] == "-r";

            // Build a symbol 'heatmap' by index of occurence.
            var dict = new SortedDictionary<int, int>();
            foreach (var line in lines)
            {
                for (int j = 0; j < line.Length; j++)
                {
                    var k = fromEnd ? line.Length - 1 - j : j;
                    if (IsOther(line[k]))
                    {
                        // Increment entry in symbol heatmap.
                        if (!dict.ContainsKey(j))
                        {
                            dict.Add(j, 0);
                        }
                        dict[j]++;
                    }
                }
            }

            // Print out total.
            var totalCount = lines.Count(x => x.Any(y => IsOther(y)));
            Console.WriteLine($"A total of {totalCount}/{lines.Count()} contain symbol" +
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
