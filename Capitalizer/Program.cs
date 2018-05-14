using System;
using System.Collections.Generic;
using System.IO;

using Capitalizer.Shared;

namespace Capitalizer
{
    class Program
    {
        static void Main(string[] args)
        {
            // Print usage.
            if (args.Length < 1)
            {
                Console.WriteLine("Usage: Capitalizer <input_file>");
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

            // For each line in the file.
            var output = new List<string>();
            foreach (var line in lines)
            {
                // Capitalzie first letter in string (if any).
                var capitalized = "";
                var capitalInserted = false;
                for (int i = 0; i < line.Length; i++)
                {
                    if (char.IsLetter(line[i]) && !capitalInserted)
                    {
                        capitalized += char.ToUpper(line[i]);
                        capitalInserted = true;
                    }
                    else
                    {
                        capitalized += line[i];
                    }
                }

                // Add capitalized string to output.
                output.Add(capitalized);
            }

            // Write out result.
            foreach (var str in output)
            {
                Console.WriteLine(str);
            }
        }
    }
}
