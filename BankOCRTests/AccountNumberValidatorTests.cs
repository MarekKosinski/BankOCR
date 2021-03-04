using BankOCR.Implementations;
using Xunit;

namespace BankOCRTests
{
    public class AccountNumberValidatorTests
    {
        AccountNumberValidator _numberValidator;
        public AccountNumberValidatorTests()
        {
            _numberValidator = new AccountNumberValidator();
        }

        [Theory]
        [InlineData("000000051")]
        public void ShouldReturnTrue_WhenValidAccountNumber(string accountNumber)
        {
            Assert.True(_numberValidator.IsValidAccountNumber(accountNumber));
        }

        [Theory]
        [InlineData("664371495")]
        public void ShouldReturnFalse_WhenInvalidAccountNumber(string accountNumber)
        {
            Assert.False(_numberValidator.IsValidAccountNumber(accountNumber));
        }

    }
}
