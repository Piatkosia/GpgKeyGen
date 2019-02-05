using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpgKeyGenWrapper
{
    public class GpgKeygenParams
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ExpiredInDays { get; set; } //0 if never
        public string KeyType { get; set; } = "RSA"; //we can change in the future
        public uint KeyLength { get; set; } = 2048;
        public string PublicKeyPath { get; set; } = "pubkey.pub";
        public string PrivateKeyPath { get; set; } = "privkey.sec";
    }
}
