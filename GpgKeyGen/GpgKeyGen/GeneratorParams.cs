using System;
using System.Collections.Generic;
using System.Linq;
using GeneralUtils;
using GpgKeyGenWrapper;

namespace GpgKeyGen
{
    public class GeneratorParams
    {
        public string Username { get; set; }
        public string Comment { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool ForCommission { get; set; }
        public string ErrorLog { get; set; }

        public GpgKeygenParams ToGpgKeygenParams()
        {
            return new GpgKeygenParams()
            {
                Comment = this.Comment,
                Email = this.Email,
                ExpiredInDays = ForCommission ? 1 : 0,
                Password = this.Password,
                Username = this.Username,
            };
        }

        public bool IsValid()
        {
            ErrorLog = String.Empty;
            bool result = true;
            if (String.IsNullOrWhiteSpace(Username))
            {
                result = false;
                ErrorLog += "Dane osobowe nie zostały ustawione.\n";
            }
            if (String.IsNullOrWhiteSpace(Comment))
            {
                result = false;
                ErrorLog += "Komentarz nie został ustawiony" + System.Environment.NewLine;
            }
            if (!EmailUtils.IsValidAddress(Email))
            {
                result = false;
                ErrorLog += "E-mail jest nieustawiony lub jest niepoprawny." + System.Environment.NewLine;
            }

            if (string.IsNullOrWhiteSpace(Password))
            {
                result = false;
                ErrorLog += "Hasło jest nieustawione." + System.Environment.NewLine;
            }
            return result;
        }
    }
}
