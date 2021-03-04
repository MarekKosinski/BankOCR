using System;
using System.Collections.Generic;
using System.Text;

namespace BankOCR.Interfaces
{
    public interface IAccountNumberValidator
    {
        bool IsValidAccountNumber(string number);
    }
}
