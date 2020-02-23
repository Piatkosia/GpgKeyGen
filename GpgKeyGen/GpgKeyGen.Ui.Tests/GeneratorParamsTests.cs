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
            par.Limited = true;
            par.Username = "asygfuysgfigreg";
            var tosend = par.ToGpgKeygenParams();
            Assert.True(tosend.ExpiredInDays != 0);
        }
        [Fact]
        public void ParamsConverter_IfNotForCommission_Returns0asExpiredInDays()
        {
            GeneratorParamsModel par = new GeneratorParamsModel();
            par.Comment = "usdvuyfsgdf";
            par.Email = "iasgf@sd.pl";
            par.Password = "uiadfusdif";
            par.Limited = false;
            par.Username = "asygfuysgfigreg";
            var tosend = par.ToGpgKeygenParams();
            Assert.True(tosend.ExpiredInDays == 0);
        }
    }
}
