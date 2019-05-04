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
    public class HexNumberCorrect : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Dane nieprawidłowe");
            if (value != null && IsHexNumber(value.ToString())) validationResult = ValidationResult.ValidResult;
            return validationResult;
        }
        private bool IsHexNumber(string stringValue)
        {
            string nameregex = "^[a-fA-F0-9]+$";
            Match match = Regex.Match(stringValue, nameregex);
            return match.Success;
        }
    }
}
