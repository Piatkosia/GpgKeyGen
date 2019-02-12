using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace GpgKeyGen.Validators
{
    class NetAddressCorrect : ValidationRule
    {
        public bool AllowEmpty { get; set; }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Adres nieprawidłowy");
            if (value != null)
            {
                string stringValue = value as string;
                if (IsValidAddress(stringValue) || (String.IsNullOrWhiteSpace(stringValue) && AllowEmpty))
                {
                    validationResult = ValidationResult.ValidResult;
                }
            }

            if (value == null && AllowEmpty)
            {
                validationResult = ValidationResult.ValidResult;
            }
            return validationResult;
        }

        private bool IsValidAddress(string stringValue)
        {
            string ipregex =
                "^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            Match match = Regex.Match(stringValue, ipregex);
            return (match.Success || Uri.IsWellFormedUriString(stringValue, UriKind.RelativeOrAbsolute));

        }
    }
}
