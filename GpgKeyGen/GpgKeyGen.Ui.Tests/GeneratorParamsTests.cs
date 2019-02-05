using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace GpgKeyGen.Ui.Tests
{
    public class GeneratorParamsTests
    {
        [Fact]
        public void ParamsConverter_IfForCommission_Returns1asExpiredInDays()
        {
            GeneratorParamsModel par = new GeneratorParamsModel();
            par.Comment = "usdvuyfsgdf";
            par.Email = "iasgf@sd.pl";
            par.Password = "uiadfusdif";
            par.OneDay = true;
            par.Username = "asygfuysgfigreg";
            var tosend = par.ToGpgKeygenParams();
            Assert.True(tosend.ExpiredInDays == 1);
        }
        [Fact]
        public void ParamsConverter_IfNotForCommission_Returns0asExpiredInDays()
        {
            GeneratorParamsModel par = new GeneratorParamsModel();
            par.Comment = "usdvuyfsgdf";
            par.Email = "iasgf@sd.pl";
            par.Password = "uiadfusdif";
            par.OneDay = false;
            par.Username = "asygfuysgfigreg";
            var tosend = par.ToGpgKeygenParams();
            Assert.True(tosend.ExpiredInDays == 0);
        }
    }
}
