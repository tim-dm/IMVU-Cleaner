using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public class CacheCleaner : ICleaner
    {
        private static string _imvuPath;
        private static string _clientPath;

        private List<string> _cachePaths = new();

        public CacheCleaner(string imvuPath, string clientPath)
        {
            _imvuPath = imvuPath;
            _clientPath = clientPath;

            _cachePaths = new()
            {
                Path.Join(_clientPath, @"ui\profile\Cache"),
                Path.Join(_imvuPath, @"Cache"),
                Path.Join(_imvuPath, @"AssetCache"),
                Path.Join(_imvuPath, @"HttpCache"),
                Path.Join(_imvuPath, @"PixmapCache"),
                Path.Join(_imvuPath, @"avpics"),
            };
        }

        public void Clean()
        {
            DisplayTitle();
            CleanFolders();
            CleanFiles();
        }

        /// <summary>
        /// Deletes all files in each path in <see cref="_cachePaths" />
        /// </summary>
        private void CleanFolders()
        {
            foreach (string path in _cachePaths)
            {
                if (!Directory.Exists(path))
                {
                    continue;
                }

                Console.WriteLine($"Cleaning {path}");

                foreach (string file in GetFiles(path, true))
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// Deletes the .cache files and the inventory databade cache file from the IMVU folder
        /// </summary>
        private static void CleanFiles()
        {
            if(Directory.Exists(_imvuPath))
            {
                string[] files = Directory.GetFiles(_imvuPath, "*.*")
                    .Where(f => Path.GetExtension(f).Equals(".cache") || Path.GetFileName(f).ToLower().Equals("productInfoCache.db")).ToArray();

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }

        /// <summary>
        /// Grabs all the files in the specified path
        /// </summary>
        /// <param name="path">The directory to scan</param>
        /// <returns>A list of file paths</returns>
        private static IEnumerable<string> GetFiles(string path, bool includeFolders = false)
        {
            List<string> buffer = new();

            if(Directory.Exists(path))
            {
                foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.AllDirectories))
                {
                    buffer.Add(file);
                }

                if(includeFolders)
                {
                    foreach (string file in Directory.GetDirectories(path))
                    {
                        buffer.Add(file);
                    }
                }
            }

            return buffer;
        }

        private static void DisplayTitle()
        {
            Console.WriteLine("╔==============================================╗");
            Console.WriteLine("║           Cleaning your IMVU Cache           ║");
            Console.WriteLine("╚==============================================╝");
        }
    }
}
