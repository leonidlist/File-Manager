using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FolderUp
    {
        public string Name { get; private set; }

        public FolderUp()
        {
            Name = "..";
        }
    }
}
