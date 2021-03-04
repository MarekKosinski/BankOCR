using BankOCR.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using BankOCR.Constants;
using System.Linq;

namespace BankOCR.Implementations
{
    public class AccountNumberParser : IAccountNumberParser
    {
        private readonly Dictionary<string, string> NumberCodesDictionary = new Dictionary<string, string>()
        {
            { AccountNumberCodes.Zero, "0"},
            { AccountNumberCodes.One, "1"},
            { AccountNumberCodes.Two, "2"},
            { AccountNumberCodes.Three, "3"},
            { AccountNumberCodes.Four, "4"},
            { AccountNumberCodes.Five, "5"},
            { AccountNumberCodes.Six, "6"},
            { AccountNumberCodes.Seven, "7"},
            { AccountNumberCodes.Eight, "8"},
            { AccountNumberCodes.Nine, "9"},
        };
        private const int CharacterWidth = 3;
        private const int SublinesInLine = 3;
        private const int CharactersInSubline = 27;

        public string ParseNumberCode(string code)
        {
            return NumberCodesDictionary.ContainsKey(code) ? NumberCodesDictionary[code] : AccountNumberCodes.Error;
        }

        public string ParseAccountNumberLine(string line)
        {
            var sublines = line.Split(Environment.NewLine);

            if (sublines.Length != SublinesInLine)
            {
                throw new ArgumentOutOfRangeException("Line consists of incorrect number of sublines.");
            }

            if(sublines.Any(sl => sl.Length != CharactersInSubline))
            {
                throw new ArgumentOutOfRangeException("Subline consists of incorrect number of characters");
            }

            var stringBuilder = new StringBuilder();

            for(var i = 0; i < CharactersInSubline; i+= CharacterWidth)
            {
                var character = ParseNumberCode(new string (sublines.SelectMany(sl => sl.Substring(i, CharacterWidth)).ToArray()));
                if (character == AccountNumberCodes.Error)
                    return AccountNumberCodes.Error;

                stringBuilder.Append(character);
            }
            return stringBuilder.ToString();
        }
    }
}
