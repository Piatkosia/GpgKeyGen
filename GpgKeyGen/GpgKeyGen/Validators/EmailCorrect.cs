using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GpgKeyGen.Validators
{
    public class EmailCorrect : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Adres nieprawidłowy");
            if (value != null)
            {
                string stringValue = value as string;
                if (IsValid(stringValue))
                {
                    validationResult = ValidationResult.ValidResult;
                }
            }
            return validationResult;
        }
        string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                                   + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                                   + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

        private bool IsValid(string stringValue)
        {
            if (string.IsNullOrWhiteSpace(stringValue)) return false;
            return new Regex(validEmailPattern, RegexOptions.IgnoreCase).IsMatch(stringValue);
        }
    }
}
