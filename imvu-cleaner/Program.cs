using System;
using System.Collections.Generic;
using System.IO;

namespace IMVU_Cleaner
{
    class Program
    {
        private static readonly string _appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string _imvuPath = Path.Join(_appData, @"\IMVU\");
        private static readonly string _clientPath = Path.Join(_appData, @"\IMVUClient\");

        private static bool _idle = true;
        private static readonly List<ICleaner> _cleaners = new();

        static void Main(string[] args)
        {
            DisplayMenu();

            while (_idle)
            {
                ConsoleKeyInfo pressedKey = Console.ReadKey(true);

                if (!char.IsNumber(pressedKey.KeyChar))
                {
                    continue;
                }

                if (int.TryParse(pressedKey.KeyChar.ToString(), out int number))
                {
                    switch (number)
                    {
                        case 1:
                            _cleaners.Add(new CacheCleaner(_imvuPath, _clientPath));
                            _idle = false;
                            break;

                        case 2:                            
                            _cleaners.Add(new LogCleaner(_imvuPath));
                            _idle = false;
                            break;
                        case 3:                            
                            _cleaners.Add(new CacheCleaner(_imvuPath, _clientPath));
                            _cleaners.Add(new LogCleaner(_imvuPath));
                            break;
                        case 4:
                            _idle = false;
                            break;
                    }
                }
            }

            Console.Clear();

            foreach (ICleaner cleaner in _cleaners)
            {
                cleaner.Clean();
            }

            //Console.ReadLine();
            DisplayExitScreen();
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("╔==============================================╗");
            Console.WriteLine("║      Advanced IMVU Cleaner by Dataminer      ║");
            Console.WriteLine("║                                              ║");
            Console.WriteLine("║             1.Delete Client Cache            ║");
            Console.WriteLine("║             2.Delete Client Logs             ║");
            Console.WriteLine("║             3.Delete All                     ║");
            Console.WriteLine("║             4.Exit                           ║");
            Console.WriteLine("║                                              ║");
            Console.WriteLine("╚==============================================╝");
        }

        private static void DisplayExitScreen()
        {
            Console.Clear();
            Console.WriteLine("╔==============================================╗");
            Console.WriteLine("║                                              ║");
            Console.WriteLine("║                  COMPLETED                   ║");
            Console.WriteLine("║                                              ║");
            Console.WriteLine("║             PRESS ANY KEY TO EXIT            ║");
            Console.WriteLine("║                                              ║");
            Console.WriteLine("╚==============================================╝");
            Console.ReadLine();
        }
    }
}
