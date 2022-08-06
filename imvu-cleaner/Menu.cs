using System;
using static System.Console;

namespace IMVU_Cleaner
{
    public enum MenuOption
    {
        Empty = 0,
        ClassicClientCache = 1,
        ClassicClientLogs = 2,
        ProjectFiles = 3,
        All = 4,
        Exit = 5,
    }

    public class Menu
    {
        private const int MaxOptions = 5;
        private MenuOption _selectedOption;

        public void Display()
        {
            Clear();
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "╔====================================================╗");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║        Advanced IMVU Cleaner by Dataminer          ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║        ----------------------------------          ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║          Please make sure IMVU is closed           ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║  Please selection an option from the menu below    ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║       by pressing a number on your keyboard        ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║             1. Delete Client Cache                 ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║             2. Delete Client Logs                  ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║             3. Project Files                       ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║             4. Delete All                          ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║             5. Exit                                ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "║                                                    ║");
            ConsoleUtils.WriteLineColored(ConsoleColor.Cyan, "╚====================================================╝");
        }

        public MenuOption GetSelectedOption()
        {
            ConsoleKeyInfo keyPressed;
           
            keyPressed = ReadKey(true);

            if (int.TryParse(keyPressed.KeyChar.ToString(), out int number))
            {
                if(number == 0 || number > MaxOptions)
                {
                    return MenuOption.Empty;
                }

                _selectedOption = (MenuOption)number;
            }           

            return _selectedOption;
        }
    }
}
