using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;
using GeneralUtils;

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
                if (EmailUtils.IsValidAddress(stringValue))
                {
                    validationResult = ValidationResult.ValidResult;
                }
            }
            return validationResult;
        }
    }
}
