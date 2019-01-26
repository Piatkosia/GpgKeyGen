using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace GeneralUtils
{
    public class EmailUtils
    {
        public static readonly string ValidEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        public static bool IsValidAddress(string mailAddress)
        {
            if (string.IsNullOrWhiteSpace(mailAddress))
            {
                return false;
            }
            return new Regex(ValidEmailPattern, RegexOptions.IgnoreCase).IsMatch(mailAddress);
        }

    }
}
