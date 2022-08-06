using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public class CleanerManager
    {
        private static readonly string _appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static readonly string _imvuPath = Path.Join(_appData, @"\IMVU\");
        private static readonly string _classicClientPath = Path.Join(_appData, @"\IMVUClient\");

        public static List<ICleaner> GetCleaners(MenuOption menuOption)
        {
            List<ICleaner> cleaners = new();

            switch (menuOption)
            {
                default:
                case MenuOption.ClassicClientCache:
                    cleaners.Add(new ClassicCacheCleaner(_imvuPath, _classicClientPath));
                    break;

                case MenuOption.ClassicClientLogs:
                    cleaners.Add(new ClassicLogCleaner(_imvuPath));
                    break;

                case MenuOption.ProjectFiles:
                    cleaners.Add(new ProjectCleaner());
                    break;

                case MenuOption.All:
                    cleaners.Add(new ClassicCacheCleaner(_imvuPath, _classicClientPath));
                    cleaners.Add(new ClassicLogCleaner(_imvuPath));
                    cleaners.Add(new ProjectCleaner());
                    break;
            }

            return cleaners;
        }
    }
}
