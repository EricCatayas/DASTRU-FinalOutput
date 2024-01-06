using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace FinalOutput
{
    public class GetFilePath
    {
        public static string TextFilePath(string path, string fileName)
        {
            return Path.GetFullPath(path) + $"\\{fileName}.txt";
        }

        /// <summary>
        /// Returns the full path of the given argument
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string FilePath(string path)
        {
            return Path.GetFullPath(path);
        }

    }
}
