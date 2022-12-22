using System;
using System.Collections.Generic;
using System.IO;

namespace IMVU_Cleaner
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "IMVU Cleaner v1.0 by Datamine";

            Menu menu = new();
            menu.Display();

            MenuOption option;            

            do
            {
                option = menu.GetSelectedOption();
            }
            while (option == MenuOption.Empty); 

            if (option == MenuOption.Exit)
            {
                Environment.Exit(0);
            }

            Console.Clear();

            foreach (ICleaner cleaner in CleanerManager.GetCleaners(option))
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

            DisplayExitScreen();
        }

        private static void DisplayExitScreen()
        {
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "╔==============================================╗");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "║                                              ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "║                  COMPLETED                   ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "║                                              ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "║             PRESS ANY KEY TO EXIT            ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "║                                              ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Green, "╚==============================================╝");
            Console.ReadLine();
        }        
    }
}
