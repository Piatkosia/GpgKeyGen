using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpgKeyGenWrapper
{
    public class GpgKeygenParams
    {
        public static readonly string DefaultKeyType = "RSA";
        public static readonly uint DefaultKeyLength = 2048;
        public static readonly string DefaultPublicKeyFilename = "pubkey.gpg";
        public static readonly string DefaultPrivateKeyFilename = "privkey.sec";
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Comment { get; set; }
        public int ExpiredInDays { get; set; } //0 if never
        public string KeyType { get; set; } = DefaultKeyType;
        public uint KeyLength { get; set; } = DefaultKeyLength;
        public string PublicKeyPath { get; set; } = DefaultPublicKeyFilename;
        public string PrivateKeyPath { get; set; } = DefaultPrivateKeyFilename;
    }
}
