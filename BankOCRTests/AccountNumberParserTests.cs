using BankOCR.Constants;
using BankOCR.Implementations;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BankOCRTests
{
    public class AccountNumberParserTests
    {
        AccountNumberParser _numberScanner;
        public AccountNumberParserTests()
        {
            _numberScanner = new AccountNumberParser();
        }

        public class ParseNumberCodeTests : AccountNumberParserTests
        {
            [Theory]
            [InlineData("0", " _ "
                           + "| |"
                           + "|_|")]
            [InlineData("1", "   "
                           + "  |"
                           + "  |")]
            [InlineData("2", " _ "
                           + " _|"
                           + "|_ ")]
            [InlineData("3", " _ "
                           + " _|"
                           + " _|")]
            [InlineData("4", "   "
                           + "|_|"
                           + "  |")]
            [InlineData("5", " _ "
                           + "|_ "
                           + " _|")]
            [InlineData("6", " _ "
                           + "|_ "
                           + "|_|")]
            [InlineData("7", " _ "
                           + "  |"
                           + "  |")]
            [InlineData("8", " _ "
                           + "|_|"
                           + "|_|")]
            [InlineData("9", " _ "
                           + "|_|"
                           + " _|")]
            public void ShouldReturnProperNumber_WhenCorrectNumberCode(string expectedNumberText, string numberCode)
            {
                Assert.Equal(expectedNumberText, _numberScanner.ParseNumberCode(numberCode));
            }

            [Theory]
            [InlineData("   |_| ||")]
            [InlineData("   |__ ||")]
            [InlineData("   |___||")]
            [InlineData("  _|_| ||")]
            public void ShouldReturnERR_WhenIncorrectNumberCode(string numberCode)
            {
                Assert.Equal(AccountNumberCodes.Error, _numberScanner.ParseNumberCode(numberCode));
            }
        }

        public class ParseAccountNumberLineTests : AccountNumberParserTests
        {

            [Theory]
            [InlineData(" _  _  _  _  _  _  _  _  _  _" + "\r\n" +
                        "| || || || || || || || || |"   + "\r\n" +
                        "|_||_||_||_||_||_||_||_||_|")]
            [InlineData(" _  ___  _  _  _  _"         + "\r\n" +
                        "| || || || || || || || || |" + "\r\n" +
                        "|_||_||_||_||_||_||_||_||_|")]
            [InlineData(" _  _  _  _  _  _  _  _  _" + "\r\n" +
                        "| || || || || || || || |||" + "\r\n" +
                        "|_||_||_||_||_||_||_||_||_|")]
            [InlineData(" _  _  _  _  _  _  _  _  _"  + "\r\n" +
                        "| || || || || || || || || |" + "\r\n" +
                        "|_||_||_||_||_||_||_||||_|")]
            public void ShouldThrowArgumentException_WhenIncorrectNumberOfCharactersInSubline(string line)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _numberScanner.ParseAccountNumberLine(line));
            }


            [Theory]
            [InlineData(" _  _  _  _  _  _  _  _  _ " + "\r\n" +
                        "| || || || || || || || || |" + "\r\n" +
                        "| || || || || || || || || |" + "\r\n" +
                        "|_||_||_||_||_||_||_||_||_|")]
            [InlineData(" _  _  _  _  _  _  _  _  _ " + "\r\n" +
                        "|_||_||_||_||_||_||_||_||_|")]
            public void ShouldThrowArgumentException_WhenIncorrectNumberOfSublines(string line)
            {
                Assert.Throws<ArgumentOutOfRangeException>(() => _numberScanner.ParseAccountNumberLine(line));
            }

            [Theory]
            [InlineData("000000000", " _  _  _  _  _  _  _  _  _ " + "\r\n" +
                                     "| || || || || || || || || |" + "\r\n" +
                                     "|_||_||_||_||_||_||_||_||_|")]
            [InlineData("123456789", "    _  _     _  _  _  _  _ " + "\r\n" +
                                     "  | _| _||_||_ |_   ||_||_|" + "\r\n" +
                                     "  ||_  _|  | _||_|  ||_| _|")]
            public void ShouldReturnProperNumberText_WhenCorrectLine(string expectedNumber, string line)
            {
                Assert.Equal(expectedNumber, _numberScanner.ParseAccountNumberLine(line));
            }

        }

    }
}
