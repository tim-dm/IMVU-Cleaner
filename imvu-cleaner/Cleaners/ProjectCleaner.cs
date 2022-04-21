using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMVU_Cleaner.Cleaners
{
    public class ProjectCleaner : ICleaner, IHasConfirmation
    {
        private readonly List<string> _projectPaths = new();

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
            foreach (string path in _projectPaths)
            {
                if(!Directory.Exists(path))
                {
                    continue;
                }
            }
        }

        public void DisplayTitle()
        {
            
        }

        public bool Confirm()
        {
            throw new NotImplementedException();
        }        
    }
}
