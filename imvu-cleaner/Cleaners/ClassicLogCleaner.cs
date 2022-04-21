using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner
{
    public class ClassicLogCleaner : ICleaner
    {
        private readonly string _imvuPath;

        private static List<string> _extensions => new()
        {
            ".log",
            ".1",
            ".2",
            ".3",
            ".4",
            ".5",
            ".6"
        };

        public ClassicLogCleaner(string imvuPath)
        {
            _imvuPath = imvuPath;
        }

        public void Clean()
        {
            if (Directory.Exists(_imvuPath))
            {
                string[] files = Directory.GetFiles(_imvuPath)
                    .Where(f => _extensions.Any(e => f.EndsWith(e, StringComparison.OrdinalIgnoreCase))).ToArray();

                foreach (string file in files)
                {
                    File.Delete(file);
                }
            }
        }
    }
}
