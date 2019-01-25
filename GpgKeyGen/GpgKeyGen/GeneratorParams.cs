using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
