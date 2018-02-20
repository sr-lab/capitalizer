﻿using System;
using System.Collections.Generic;

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
            // Read in entire file.
            var lines = FileUtils.ReadFileAsLines(args[0]);

            // Build an uppercase letter 'heatmap' by index of occurence.
            var dict = new SortedDictionary<int, int>();
            foreach (var line in lines)
            {
                // Strip all non-letters from string.
                var stripped = StripNonLetters(line);
                for (int j = 0; j < stripped.Length; j++)
                {
                    if (Char.IsUpper(stripped[j]))
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