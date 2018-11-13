using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace FileManager
{
    public enum ESection
    {
        Left, Right
    }

    public abstract class Section
    {
        public ArrayList Files { get; set; }
        public Cursor Cursor { get; private set; }
        public int X { get; private set; }
        public int Y { get; private set; }

        protected Section(int x, int y, ArrayList files, Cursor cursor)
        {
            X = x;
            Y = y;
            Files = files;
            Cursor = cursor;
        }

        public void DisplayFiles()
        {
            GUI.CleanScreen(X, Y);

            GUI.DrawFiles(Files, X, Y, 0, Config.FilesCountTwoSections);
        }
    }

    public class LeftSection : Section
    {
        public LeftSection(ArrayList files) : base(1, 7, files, new LeftSectionCursor(files)) { }
    }

    public class RightSection : Section
    {
        public RightSection(ArrayList files) : base(Config.HalfWindowWidth + 1, 7, files, new RightSectionCursor(files)) { }
    }

}
