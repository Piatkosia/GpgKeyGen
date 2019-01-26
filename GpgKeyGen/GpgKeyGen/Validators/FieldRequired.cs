using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace GpgKeyGen.Validators
{
    public class FieldRequired : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            ValidationResult validationResult = new ValidationResult(false, "Brak wartości");
            if (value != null)
            {
                string stringValue = value as string;
                if (!string.IsNullOrWhiteSpace(stringValue))
                {
                    validationResult = ValidationResult.ValidResult;
                }   
            }
            return validationResult;

        }
    }
}
