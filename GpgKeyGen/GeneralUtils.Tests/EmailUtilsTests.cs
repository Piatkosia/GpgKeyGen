using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GeneralUtils.Tests
{
    public class EmailUtilsTests
    {
        [Theory]
        [InlineData(null, false)]
        [InlineData("random data", false)]
        [InlineData("kajfdhsduyfusahf", false)]
        [InlineData("aaa.aa@sd.as", true)]
        public void IsValidAdress_ValidProperly(string address, bool expected)
        {
            Assert.True(EmailUtils.IsValidAddress(address) == expected);
        }
    }
}
