using BankOCR.Implementations;
using BankOCR.Interfaces;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;

namespace BankOCR
{
    class Program
    {
        private readonly static IAccountNumberParser _parser = new AccountNumberParser();
        private readonly static IAccountNumberValidator _validator = new AccountNumberValidator();
        private const int SublinesInAccountNumber = 4;
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Clear();
                Console.WriteLine("Input the file path to account numbers file.");
                var path = Console.ReadLine();
                if (string.IsNullOrEmpty(path) || !File.Exists(path))
                {
                    Console.WriteLine("File does not exists");
                    Console.ReadKey();
                    continue;
                }
                ReadAccountNumbersFile(path);
                Console.ReadKey();
            }
        }

        private static void ReadAccountNumbersFile(string path)
        {
            var lines = File.ReadLines(path).ToArray();
            var stringBuilder = new StringBuilder();
            for (var i = 0; i < lines.Count(); i++)
            {
                if ((i + 1) % SublinesInAccountNumber == 0)
                {
                    if (stringBuilder.Length > 0)
                    {
                        stringBuilder.Remove(stringBuilder.ToString().Length - Environment.NewLine.Length, Environment.NewLine.Length);
                        var parsedAccountNumber = _parser.ParseAccountNumberLine(stringBuilder.ToString());
                        var isValidText = _validator.IsValidAccountNumber(parsedAccountNumber) ? "" : "ERR";
                        Console.WriteLine($"{parsedAccountNumber} {isValidText}");
                        stringBuilder.Clear();
                    }
                }
                else
                {
                    stringBuilder.Append(lines[i] + Environment.NewLine);
                }
            }
        }
    }
}
