using NTemplates;
using System;
using System.Diagnostics;

namespace DocumentGenerator
{
    public class KeyIntroduceGenerator
    {
        public static readonly string inputPath = @"./DocumentTemplates/N_cert.rtf";
        public void GenerateDocument(string path, string keyID, string username)
        {
            DocumentCreator dc = new DocumentCreator();
            
            dc.AddString("Date", DateTime.Now.ToShortDateString());
            dc.AddString("keyID", keyID);
            var outputPath = $"{path}//Oswiadczenie.rtf";
            dc.CreateDocument(inputPath, outputPath);
            Process.Start(outputPath);
        }
    }
}
