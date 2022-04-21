using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public static class FileUtils
    {
        /// <summary>
        /// Grabs all the files in the specified path
        /// </summary>
        /// <param name="path">The directory to scan</param>
        /// <returns>A list of file paths</returns>
        public static IEnumerable<string> GetFiles(string path, bool includeFolders = false)
        {
            List<string> buffer = new();

            if (Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    buffer.Add(file);
                }

                if (includeFolders)
                {
                    foreach (string file in Directory.GetDirectories(path))
                    {
                        buffer.Add(file);
                    }
                }
            }

            return buffer;
        }
    }
}
