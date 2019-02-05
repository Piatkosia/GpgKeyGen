using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GpgKeyGenWrapper
{
    public class GpgBatchGenerator
    {
        public static string GetScript(GpgKeygenParams keygenParams)
        {
            string output_string;
            output_string = $"Key-Type: {keygenParams.KeyType}\n"; 
            output_string += $"Key-Length: {keygenParams.KeyLength}\n";
            output_string += $"Name-Real: {keygenParams.Username}\n";
            output_string += $"Name-Comment: {keygenParams.Comment}\n";
            output_string += $"Name-Email: {keygenParams.Email}\n";
            output_string += $"Expire-Date: {keygenParams.ExpiredInDays}\n";
            output_string += $"Passphrase: {keygenParams.Password}\n";
            output_string += $"%pubring {keygenParams.PublicKeyPath}\n";
            output_string += $"%secring {keygenParams.PrivateKeyPath}\n";
            output_string += $"%commit";
            return output_string;

        }

        public static void GenerateKey(GpgKeygenParams gpgKeygenParams)
        {
            throw new NotImplementedException();
        }
    }
}
