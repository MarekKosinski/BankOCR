using System;
using System.Collections.Generic;
using System.Text;

namespace BankOCR.Interfaces
{
    public interface IAccountNumberParser
    {
        string ParseAccountNumberLine(string line);
        string ParseNumberCode(string code);
    }
}
