using BankOCR.Interfaces;
using System;
using System.Linq;

namespace BankOCR.Implementations
{
    public class AccountNumberValidator : IAccountNumberValidator
    {
        private const int AccountNumberLength = 9;

        public bool IsValidAccountNumber(string accountNumber)
        {
            if (IsInvalidFormat(accountNumber)  || !IsAllDigits(accountNumber))
            {
                return false;
            }

            return IsValidChecksum(accountNumber.Select(c => c - '0').ToArray());
        }
        private bool IsValidChecksum(int[] numbers)
        {
            return Enumerable.Range(1, AccountNumberLength).
               Aggregate(0, (sum, position) => sum + position * numbers[AccountNumberLength - position]) % 11 == 0;
        }

        private bool IsInvalidFormat(string accountNumber)
        {
            return accountNumber.Length != AccountNumberLength;
        }
        private bool IsAllDigits(string accountNumber)
        {
            return accountNumber.All(c => char.IsDigit(c));
        }
    }
}
