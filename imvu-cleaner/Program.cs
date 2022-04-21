using System;
using System.Collections.Generic;
using System.IO;

namespace IMVU_Cleaner
{
    public enum MenuOptions
    {
        ClassicClientCache =  1,
        ClassicClientLogs =  2,
        All =  3,
        Exit =  4,
    }

    class Program
    {
        /// <summary>
        /// The root location of the IMVU directories
        /// </summary>
        private static readonly string _appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        /// <summary>
        /// The path to the IMVU directory
        /// </summary>
        private static readonly string _imvuPath = Path.Join(_appData, @"\IMVU\");

        /// <summary>
        /// The path to the IMVUClient directory
        /// </summary>
        private static readonly string _clientPath = Path.Join(_appData, @"\IMVUClient\");

        /// <summary>
        /// Flag to determine if we're waiting for input
        /// </summary>
        private static bool _idle = true;
        
        /// <summary>
        /// Flag to determine if we should exit the program
        /// </summary>
        private static bool _exit = false;

        /// <summary>
        /// The collection of <see cref="ICleaner" /> to run
        /// </summary>
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
                        case (int)MenuOptions.ClassicClientCache:
                            _cleaners.Add(new ClassicCacheCleaner(_imvuPath, _clientPath));
                            _idle = false;
                            break;

                        case (int)MenuOptions.ClassicClientLogs:                            
                            _cleaners.Add(new ClassicLogCleaner(_imvuPath));
                            _idle = false;
                            break;

                        case (int)MenuOptions.All:                            
                            _cleaners.Add(new ClassicCacheCleaner(_imvuPath, _clientPath));
                            _cleaners.Add(new ClassicLogCleaner(_imvuPath));
                            break;

                        case (int)MenuOptions.Exit:
                            _idle = false;
                            _exit = true;
                            break;
                    }
                }
            }

            if(!_exit)
            {
                Console.Clear();

                foreach (ICleaner cleaner in _cleaners)
                {
                    try
                    {
                        cleaner.Clean();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                    }
                }

                //Console.ReadLine();
                DisplayExitScreen();
            }
        }

        private static void DisplayMenu()
        {
            Console.Clear();
            WriteLineColored(ConsoleColor.Cyan, "╔====================================================╗");
            WriteLineColored(ConsoleColor.Cyan, "║        Advanced IMVU Cleaner by Dataminer          ║");
            WriteLineColored(ConsoleColor.Cyan, "║        ----------------------------------          ║");
            WriteLineColored(ConsoleColor.Cyan, "║          Please make sure IMVU is closed           ║");
            WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            WriteLineColored(ConsoleColor.Cyan, "║  Please selection an option from the menu below    ║");
            WriteLineColored(ConsoleColor.Cyan, "║       by pressing a number on your keyboard        ║");
            WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            WriteLineColored(ConsoleColor.Cyan, "║             1. Delete Client Cache                 ║");
            WriteLineColored(ConsoleColor.Cyan, "║             2. Delete Client Logs                  ║");
            WriteLineColored(ConsoleColor.Cyan, "║             3. Delete All                          ║");
            WriteLineColored(ConsoleColor.Cyan, "║             4. Exit                                ║");
            WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            WriteLineColored(ConsoleColor.Cyan, "╚====================================================╝");
        }

        private static void DisplayExitScreen()
        {
            Console.Clear();
            WriteLineColored(ConsoleColor.Green, "╔==============================================╗");
            WriteLineColored(ConsoleColor.Green, "║                                              ║");
            WriteLineColored(ConsoleColor.Green, "║                  COMPLETED                   ║");
            WriteLineColored(ConsoleColor.Green, "║                                              ║");
            WriteLineColored(ConsoleColor.Green, "║             PRESS ANY KEY TO EXIT            ║");
            WriteLineColored(ConsoleColor.Green, "║                                              ║");
            WriteLineColored(ConsoleColor.Green, "╚==============================================╝");
            Console.ReadLine();
        }

        private static void WriteLineColored(ConsoleColor color, string text)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
}
