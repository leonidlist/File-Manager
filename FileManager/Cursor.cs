using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace FileManager
{
    public abstract class Cursor
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Count { get; private set; }
        public ArrayList Files { get; set; }
        public string LineBefore { get; private set; }
        public int Index { get; set; }
        public List<string> FileNames { get; set; }

        protected Cursor(int x, int y, ArrayList files)
        {
            X = x;
            Y = y;
            Files = files;
            Count = Files.Count;
            Index = 0;
            FileNames = new List<string>(Files.Count);

            foreach (var i in Files)
            {
                if (i is DirectoryInfo)
                    FileNames.Add((i as DirectoryInfo).Name);
                else if (i is FileInfo)
                    FileNames.Add((i as FileInfo).Name);
                else if (i is FolderUp)
                    FileNames.Add((i as FolderUp).Name);
            }
        }

        public Cursor(ArrayList arr)
        {
            Files = arr;
            Count = Files.Count;
        }

        public virtual void MoveUp()
        {
            if (Index <= 0)
                return;

            Console.SetCursorPosition(X, Y);
            LineBefore = FileNames[Index--];

            Console.Write(" " + GUI.GetNormalString(LineBefore, Config.QuarterWindowWidth - 2));
            Console.BackgroundColor = Config.AdditionalBackgroundColor;

            if (Index == (Config.FilesCountOneSection - 1) + Files.Count - Count)
            {
                X -= Config.QuarterWindowWidth;
                Y += Config.FilesCountOneSection;
            }
            else if (Index == Files.Count - Count - 1)
            {
                Console.BackgroundColor = Config.BackgroundColor;
                GUI.CleanScreen(X, Y);
                GUI.DrawFiles(Files, X, Y, (Files.Count - Count) - 1, Config.FilesCountTwoSections);
                ++Count;
                ++Y;
                Console.BackgroundColor = Config.AdditionalBackgroundColor;
            }

            Console.SetCursorPosition(X, --Y);
            Console.Write(GUI.CreateRect(FileNames[Index], Config.QuarterWindowWidth - 2));

            Console.BackgroundColor = Config.BackgroundColor;
        }

        public virtual void MoveDown()
        {
            if (Index >= Files.Count - 1)
                return;

            Console.SetCursorPosition(X, Y);
            LineBefore = FileNames[Index++];

            Console.Write(" " + GUI.GetNormalString(LineBefore, Config.QuarterWindowWidth - 2));
            Console.BackgroundColor = Config.AdditionalBackgroundColor;

            if (Index == Config.FilesCountOneSection + Files.Count - Count)
            {
                X += Config.QuarterWindowWidth;
                Y -= Config.FilesCountOneSection;
            }
            else if (Index >= Config.FilesCountTwoSections + Files.Count - Count)
            {
                Console.BackgroundColor = Config.BackgroundColor;
                GUI.CleanScreen(X - Config.QuarterWindowWidth, Y - (Config.FilesCountOneSection - 1));
                GUI.DrawFiles(Files, X - Config.QuarterWindowWidth, Y - (Config.FilesCountOneSection - 1), (Files.Count - Count) + 1, Config.FilesCountTwoSections);
                --Count;
                --Y;
                Console.BackgroundColor = Config.AdditionalBackgroundColor;
            }

            Console.SetCursorPosition(X, ++Y);
            Console.Write(GUI.CreateRect(FileNames[Index], Config.QuarterWindowWidth - 2));

            Console.BackgroundColor = Config.BackgroundColor;
        }

        public void HideSelection()
        {
            Console.SetCursorPosition(X, Y);
            LineBefore = FileNames[Index];

            Console.Write(" " + GUI.GetNormalString(LineBefore, Config.QuarterWindowWidth - 2), Config.QuarterWindowWidth - 2);
        }

        public void UnmaskSelection()
        {
            Console.SetCursorPosition(X, Y);

            Console.BackgroundColor = Config.AdditionalBackgroundColor;
            Console.Write(GUI.CreateRect(FileNames[Index], Config.QuarterWindowWidth - 2));

            Console.BackgroundColor = Config.BackgroundColor;
        }
    }

    public class LeftSectionCursor : Cursor
    {
        public LeftSectionCursor(ArrayList files) : base(1, 7, files) { }

        public override void MoveUp()
        {
            base.MoveUp();
            GUI.DrawFileInformation(Files[Index], ESection.Left);
        }

        public override void MoveDown()
        {
            base.MoveDown();
            GUI.DrawFileInformation(Files[Index], ESection.Left);
        }
    }

    public class RightSectionCursor : Cursor
    {
        public RightSectionCursor(ArrayList files) : base(Config.HalfWindowWidth + 1, 7, files) { }

        public override void MoveUp()
        {
            base.MoveUp();
            GUI.DrawFileInformation(Files[Index], ESection.Right);
        }

        public override void MoveDown()
        {
            base.MoveDown();
            GUI.DrawFileInformation(Files[Index], ESection.Right);
        }
    }
}
