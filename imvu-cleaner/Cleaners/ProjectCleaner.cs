using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public class ProjectCleaner : ICleaner
    {
        private readonly List<string> _projectPaths = new();

        /// <summary>
        /// Flag to determine if we're waiting for input
        /// </summary>
        private static bool _idle = true;

        public ProjectCleaner()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            _projectPaths = new()
            {
                Path.Join(documentsPath, @"IMVU Projects"),
                Path.Join(documentsPath, @"IMVU Studio Projects"),
            };
        }

        public void Clean()
        {
            if (!Confirm())
            {
                return;
            }

            DisplayTitle();

            foreach (string path in _projectPaths)
            {
                if(!Directory.Exists(path))
                {
                    continue;
                }

                foreach (string file in FileUtils.GetFiles(path, true))
                {
                    if (Directory.Exists(file))
                    {
                        Console.WriteLine($"Deleting {Path.GetDirectoryName(file)}");
                        Directory.Delete(file, true);
                    }
                    else
                    {
                        Console.WriteLine($"Deleting {Path.GetFileName(file)}");
                        File.Delete(file);
                    }
                }
            }
        }

        private bool Confirm()
        {
            DisplayConfirmation();

            while (_idle)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                if (pressedKey.Key == ConsoleKey.Y)
                {
                    return true;
                }
                else
                {
                    _idle = false;
                }
            }

            return false;
        }

        private static void DisplayTitle()
        {
            Console.WriteLine("╔==============================================╗");
            Console.WriteLine("║             Cleaning Project Files           ║");
            Console.WriteLine("╚==============================================╝");
        }

        private static void DisplayConfirmation()
        {
            Console.WriteLine("╔======================================================================================╗");
            Console.WriteLine("║                                        WARNING!                                      ║");
            Console.WriteLine("║                           This will delete all project files!                        ║");
            Console.WriteLine("║   Project files are all the assets (meshes, textures, animations, poses, etc.)       ║");
            Console.WriteLine("║                  that make up products you may be currently working on               ║");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("║   Please backup or move any files you want to keep before proceeding or you          ║");
            Console.WriteLine("║                    may lose products that you hadn't published yet                   ║");
            Console.WriteLine("║                                                                                      ║");
            Console.WriteLine("║                                      Continue?                                       ║");
            Console.WriteLine("║                                   Press Y to continue                                ║");
            Console.WriteLine("║                                   Press N to cancel                                  ║");
            Console.WriteLine("╚======================================================================================╝");
        }

    }
}
