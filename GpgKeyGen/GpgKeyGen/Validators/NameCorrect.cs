using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GpgKeyGen.Validators
{
    class NameCorrect: ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Dane nieprawidłowe");
            if (value != null && IsName(value.ToString())) validationResult = ValidationResult.ValidResult;
            return validationResult;
        }
        private bool IsName(string stringValue)
        {
            string nameregex =  "^[a-zA-Z0-9óąśłżźćńĘÓĄŚŁŻŹĆŃ ]+$";
            Match match = Regex.Match(stringValue, nameregex);
            return match.Success;

        }
    }
}
