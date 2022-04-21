using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public class ClassicCacheCleaner : ICleaner
    {
        private static string _imvuPath;
        private static string _clientPath;

        private readonly List<string> _cachePaths = new();

        public ClassicCacheCleaner(string imvuPath, string clientPath)
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

                foreach (string file in FileUtils.GetFiles(path, true))
                {
                    if(Directory.Exists(file))
                    {
                        Directory.Delete(file, true);
                    } 
                    else
                    {
                        File.Delete(file);
                    }
                }
            }
        }

        /// <summary>
        /// Deletes the .cache files and the inventory database cache file from the IMVU folder
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
                
        private void DisplayTitle()
        {
            Console.WriteLine("╔==============================================╗");
            Console.WriteLine("║       Cleaning The Classic Client Cache      ║");
            Console.WriteLine("╚==============================================╝");
        }
    }
}
