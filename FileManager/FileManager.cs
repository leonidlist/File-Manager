using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace FileManager
{
    public class FileManager
    {
        public ESection Esection { get; set; }
        public string FileManagerName { get; set; }

        public LeftSection LeftSection { get; set; }
        public RightSection RightSection { get; set; }

        public ArrayList LFiles { get; set; } = new ArrayList();
        public ArrayList RFiles { get; set; } = new ArrayList();

        public string LPath { get => LDirectory.FullName; }
        public string RPath { get => RDirectory.FullName; }

        public DirectoryInfo LDirectory { get; set; } = new DirectoryInfo(Config.LeftSectionPath);
        public DirectoryInfo RDirectory { get; set; } = new DirectoryInfo(Config.RightSectionPath);

        public FileManager(string fileManagerName)
        {
            Console.BackgroundColor = Config.BackgroundColor;
            Console.ForegroundColor = Config.ForegroundColor;
            Console.SetWindowSize(Config.StartupWindowWidth, Config.StartupWindowHeight);

            FileManagerName = fileManagerName;
            Esection = ESection.Left;
            Directory.SetCurrentDirectory(Config.LeftSectionPath);

            SetLeftSectionFiles();
            SetRightSectionFiles();
        }

        public void ChangeSection()
        {
            if (Esection == ESection.Right)
            {
                RightSection.Cursor.HideSelection();
                LeftSection.Cursor.UnmaskSelection();
                Console.BackgroundColor = Config.AdditionalBackgroundColor;

                Esection = ESection.Left;

                // переписать текст слева
                Console.SetCursorPosition(2, 3);
                Console.Write($"Current path: {GUI.GetNormalString(LPath, Config.HalfWindowWidth - 19)}");

                // переписать текст справа
                Console.BackgroundColor = Config.BackgroundColor;
                Console.SetCursorPosition(Config.HalfWindowWidth + 2, 3);
                Console.Write($"Current path: {GUI.GetNormalString(RPath, Config.HalfWindowWidth - 19)}");

                Directory.SetCurrentDirectory(LDirectory.FullName);
            }
            else if (Esection == ESection.Left)
            {
                RightSection.Cursor.UnmaskSelection();
                LeftSection.Cursor.HideSelection();
                Console.BackgroundColor = Config.AdditionalBackgroundColor;

                Esection = ESection.Right;

                // переписать текст справа
                Console.SetCursorPosition(Config.HalfWindowWidth + 2, 3);
                Console.Write($"Current path: {GUI.GetNormalString(RPath, Config.HalfWindowWidth - 19)}");

                // переписать текст слева
                Console.BackgroundColor = Config.BackgroundColor;
                Console.SetCursorPosition(2, 3);
                Console.Write($"Current path: {GUI.GetNormalString(LPath, Config.HalfWindowWidth - 19)}");

                Directory.SetCurrentDirectory(RDirectory.FullName);
            }
        }

        public void SetSearchResults(ArrayList results)
        {
            if (Esection == ESection.Left)
            {
                LFiles = new ArrayList();
                LFiles.Add(new FolderUp());
                LFiles.AddRange(results);

                LeftSection = new LeftSection(LFiles);
            }
            else
            {
                RFiles = new ArrayList();
                RFiles.Add(new FolderUp());
                RFiles.AddRange(results);

                RightSection = new RightSection(RFiles);
            }
        }

        public void SetLeftSectionFiles()
        {
            LFiles = new ArrayList();
            LFiles.Add(new FolderUp());
            LFiles.AddRange(LDirectory.GetDirectories());
            LFiles.AddRange(LDirectory.GetFiles());

            LeftSection = new LeftSection(LFiles);
        }

        public void SetRightSectionFiles()
        {
            RFiles = new ArrayList();
            RFiles.Add(new FolderUp());
            RFiles.AddRange(RDirectory.GetDirectories());
            RFiles.AddRange(RDirectory.GetFiles());

            RightSection = new RightSection(RFiles);
        }

        public void MoveUp()
        {
            if (Esection == ESection.Left)
                LeftSection.Cursor.MoveUp();
            else
                RightSection.Cursor.MoveUp();
        }

        public void MoveDown()
        {
            if (Esection == ESection.Left)
                LeftSection.Cursor.MoveDown();
            else
                RightSection.Cursor.MoveDown();
        }

        public void ChangeDirectory(string newPath)
        {
            if (Esection == ESection.Left)
            {
                LDirectory = new DirectoryInfo(newPath);
                SetLeftSectionFiles();
            }
            else
            {
                RDirectory = new DirectoryInfo(newPath);
                SetRightSectionFiles();
            }

            Directory.SetCurrentDirectory(newPath);
        }

        public object GetCurrentFile()
        {
            if (Esection == ESection.Left)
                return LeftSection.Cursor.Files[LeftSection.Cursor.Index];
            else
                return RightSection.Cursor.Files[RightSection.Cursor.Index];
        }

        public void DisplayFiles()
        {
            if (Esection == ESection.Left)
                LeftSection.DisplayFiles();
            else
                RightSection.DisplayFiles();
        }

        public string GetSelectedPath()
        {
            if (Esection == ESection.Left)
            {
                if (LeftSection.Cursor.Files[LeftSection.Cursor.Index] is DirectoryInfo)
                    return (LeftSection.Cursor.Files[LeftSection.Cursor.Index] as DirectoryInfo).FullName;
                else if (LeftSection.Cursor.Files[LeftSection.Cursor.Index] is FileInfo)
                    return (LeftSection.Cursor.Files[LeftSection.Cursor.Index] as FileInfo).FullName;
                else
                    return LeftSection.Cursor.FileNames[LeftSection.Cursor.Index];               
            }
            else
            {
                if (RightSection.Cursor.Files[RightSection.Cursor.Index] is DirectoryInfo)
                    return (RightSection.Cursor.Files[RightSection.Cursor.Index] as DirectoryInfo).FullName;
                else if (RightSection.Cursor.Files[RightSection.Cursor.Index] is FileInfo)
                    return (RightSection.Cursor.Files[RightSection.Cursor.Index] as FileInfo).FullName;
                else
                    return RightSection.Cursor.FileNames[RightSection.Cursor.Index];
            }
        }

        public void Reload()
        {
            SetLeftSectionFiles();
            SetRightSectionFiles();

            GUI.DrawMenu(FileManagerName);

            LeftSection.DisplayFiles();
            RightSection.DisplayFiles();

            GUI.DrawName();
        }

        public void DeleteCurrentFile()
        {
            if (GetCurrentFile() is FileInfo)
                File.Delete(GetSelectedPath());
            else if (GetCurrentFile() is DirectoryInfo)
                Directory.Delete(GetSelectedPath());
            else
                return;
        }

        public void MoveCurrentFile()
        {
            if (Esection == ESection.Left)
            {
                if (GetCurrentFile() is FileInfo)
                    (GetCurrentFile() as FileInfo).MoveTo(RPath + "\\" + GetSelectedPath());
                else if (GetCurrentFile() is DirectoryInfo)
                    (GetCurrentFile() as DirectoryInfo).MoveTo(RPath + "\\" + GetSelectedPath());
                else
                    return;
            }
            else
            {
                if (GetCurrentFile() is FileInfo)
                    (GetCurrentFile() as FileInfo).MoveTo(LPath + "\\" + GetSelectedPath());
                else if (GetCurrentFile() is DirectoryInfo)
                    (GetCurrentFile() as DirectoryInfo).MoveTo(LPath + "\\" + GetSelectedPath());
                else
                    return;
            }
        }

        public void DisplayFilesFromTwoSections()
        {
            LeftSection.DisplayFiles();
            RightSection.DisplayFiles();
        }

        public string GetFileInfo()
        {
            string inf = "";

            if (GetSelectedPath() == "..")
                throw new Exception("This is not file");

            if (GetCurrentFile() is FileInfo)
            {
                double length = (GetCurrentFile() as FileInfo).Length / Math.Pow(1024, 2);
                string creation = (GetCurrentFile() as FileInfo).CreationTimeUtc.ToShortDateString();
                string creationTime = (GetCurrentFile() as FileInfo).CreationTimeUtc.ToShortTimeString();
                inf += $"Size: {length.ToString("F3")} MB. Creation time: {creation} {creationTime}";
            }
            else
            {
                Counter c = new Counter();
                c.CountSize(new DirectoryInfo(GetSelectedPath()));
                c.Count(new DirectoryInfo(GetSelectedPath()));

                double length = c.FolderSize;
                int fcount = c.FilesCount;
                int dcount = c.DirectoriesCount;
                inf += $"Size: {length.ToString("F3")} MB. Files: {fcount}. Folders: {dcount}";
            }

            return inf;
        }

        public void CreateFolder(string name)
        {
            if (name.Length < 1)
                return;

            Directory.CreateDirectory(name);
        }

        public void CreateFile(string name)
        {
            if (name.Length < 1)
                return;

            FileStream fs = new FileStream(name, FileMode.CreateNew, FileAccess.Write, FileShare.Inheritable);
            fs.Close();
        }

        public string GetCurrentPath()
        {
            if (Esection == ESection.Left)
                return LPath;
            else
                return RPath;
        }
    }
}
