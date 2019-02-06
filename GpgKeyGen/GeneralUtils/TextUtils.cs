using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;

namespace GeneralUtils
{
    public class TextUtils
    {
        public static string ConsoleToWPF(string stringToRecode)
        {
            if (string.IsNullOrWhiteSpace(stringToRecode)) return  String.Empty;
            var inputEncoding = System.Text.Encoding.GetEncoding(852);//852; "ISO-8859-2" , 1250
            Encoding outputEncoding = System.Text.Encoding.UTF8;
            byte[] inputBytes = inputEncoding.GetBytes(stringToRecode);
            byte[] outputBytes = Encoding.Convert(inputEncoding, outputEncoding, inputBytes);
            return outputEncoding.GetString(outputBytes);
        }
    }
}
