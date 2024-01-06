using System.IO;
using System;
using System.Text;
using System.Collections.Generic;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Diagnostics.Eventing.Reader;
using System.Security.AccessControl;

namespace FinalOutput
{
    public static class FileManager
    {
        //Could be used to let the user know the total items are there in the POS
        private static FileStream fs;
        private static StreamReader sr;



        public static IEnumerable<string[]> IterateTextFiles(IEnumerable<string> textFiles)
        {
            foreach (var txtFile in textFiles)
            {
                sr = new StreamReader(txtFile);
                string lines = sr.ReadToEnd();

                var line = lines.Split(',');

                yield return line;

                sr.Close();
                sr.Dispose();

            }


        }

        //Make a function about Clearing text file content and writinig

        

        public static void WriteTextFile(string path ,string pattern)
        {
        

            if (File.Exists(path))
            {
                File.WriteAllText(path, string.Empty);
                fs = File.Open(path, FileMode.Append);
                
                byte[] info = new UTF8Encoding(true).GetBytes(pattern);
                
                fs.Write(info, 0, info.Length);
                fs.Close();
                fs.Dispose();
            }
           
        }


        public static void CreateTextFile(string path)
        {
           
            if (!File.Exists(path))
            {
                fs = File.Create(path);
                fs.Close();
                fs.Dispose();

            }

        }

        /// <summary>
        /// Gets looks into every text files and returns it as a costumer object
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Account> GetCostumerAccounts() // TODO Transfer to other class
        {
            string path = GetFilePath.FilePath(@"Accounts\Costumer");

            var txtFiles = Directory.EnumerateFiles(path, "*.txt");

            var line = FileManager.IterateTextFiles(txtFiles);

            foreach (var property in line)
            {
                yield return new Costumer(property[0], property[1], decimal.Parse(property[2]));
                Console.WriteLine("Print Account");
            }
        }

        public static void LogCostumerAccountToTextFile(Costumer account)
        {

            string costumerPath = GetFilePath.TextFilePath(@"Accounts\Costumer", account.Username);
            CreateTextFile(costumerPath);
            WriteTextFile(costumerPath, $"{account.Username},{account.Password},{account.Balance}");
        }


    }


}
