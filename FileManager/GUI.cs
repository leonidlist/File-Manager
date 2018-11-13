using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;

namespace FileManager
{
    public static class GUI
    {
        public static void DrawHorizontalLine()
        {
            string buf = "";
            buf += "│";
            for (int i = 0; i < Config.WindowWidth; i++)
            {
                buf += "─";
            }
            buf += "│";
            Console.WriteLine(buf);
        }

        public static void DrawEmptyHorizontalLine()
        {
            string buf = "";
            buf += "│";
            for (int i = 0; i < Config.WindowWidth; i++)
            {
                buf += " ";
            }
            buf += "│";
            Console.WriteLine(buf);
        }

        public static void DrawVerticalLineBetweenTwoLines(int posX, int posY, int count)
        {
            Console.SetCursorPosition(posX, --posY);
            Console.Write("┬");
            Console.SetCursorPosition(posX, ++posY);
            for (int i = 0; i < count - 2; i++)
            {
                Console.Write("│");
                Console.SetCursorPosition(posX, posY + i);
            }
            Console.Write("┴");
        }

        public static void DrawVerticalLine(int posX, int posY, int count)
        {
            Console.SetCursorPosition(posX, posY);
            for (int i = 0; i < count; i++)
            {
                Console.Write("│");
                Console.SetCursorPosition(posX, posY + i);
            }
        }

        public static void DrawBottomPanel()
        {
            int num = Config.WindowWidth / 15;
            Console.SetCursorPosition(1, Console.WindowHeight - 1);
            Console.BackgroundColor = Config.MsgBoxBackgroundColor;
            Console.ForegroundColor = Config.MsgBoxForegroundColor;

            Console.Write("F1 View");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F2 MkFile");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F3 MkDir");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F4 Attributes");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F5 Copy");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F6 Move");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F7 Search");
            Console.SetCursorPosition(Console.CursorLeft + num, Console.WindowHeight - 1);

            Console.Write("F8 Delete");

            Console.BackgroundColor = Config.BackgroundColor;
            Console.ForegroundColor = Config.ForegroundColor;
        }

        public static void DrawMenu(string FileManagerName)
        {
            Console.SetCursorPosition(0, 0);

            string buf = "┌";
            // Верхняя полоска
            for (int i = 0; i < Config.WindowWidth; i++)
            {
                buf += "─";
            }
            buf += "┐";
            Console.WriteLine(buf);
            // ----------------------------------------------------

            Console.Write("│");
            for (int i = 0; i < Config.WindowWidth; i++)
            {
                if (i == Config.HalfWindowWidth - FileManagerName.Length / 2)
                {
                    Console.Write(FileManagerName);
                    i += FileManagerName.Length;
                }
                Console.Write(" ");
            }
            Console.WriteLine("│");
            // ----------------------------------------------------

            // Пути к файлам
            DrawHorizontalLine();
            DrawEmptyHorizontalLine();
            DrawHorizontalLine();

            // Имя
            DrawEmptyHorizontalLine();
            DrawHorizontalLine();

            // Файлы
            for (int i = 0; i < Config.FilesCountOneSection; i++)
                DrawEmptyHorizontalLine();

            DrawHorizontalLine();
            DrawEmptyHorizontalLine();

            // Нижняя полоска
            buf = "└";
            //Console.Write("└");
            for (int i = 0; i < Config.WindowWidth; i++)
            {
                buf += "─";
                //Console.Write("─");
            }
            buf += "┘";
            //Console.WriteLine("┘");
            Console.WriteLine(buf);

            // Вертикальные полосы
            DrawVerticalLineBetweenTwoLines(Config.HalfWindowWidth, 3, Console.WindowHeight - 2);

            // слева
            DrawVerticalLineBetweenTwoLines(Config.QuarterWindowWidth, 5, Console.WindowHeight - 6);

            // справа
            DrawVerticalLineBetweenTwoLines((int)(Config.WindowWidth * 0.75), 5, Console.WindowHeight - 6);
        }

        public static void DrawName()
        {
            for (int i = 0; i < 4; i++)
            {
                Console.SetCursorPosition(((int)(Config.QuarterWindowWidth) * i + 1) + 2, 5);
                Console.Write("Name");
            }
        }

        public static void DrawFileInformation(object file, ESection section)
        {
            if (section == ESection.Left)
                Console.SetCursorPosition(1, Config.WindowHeight);
            else
                Console.SetCursorPosition(Config.HalfWindowWidth + 3, Config.WindowHeight);

            if (file is FileInfo)
                Console.Write($"{(file as FileInfo).Length,+30}KBytes {(file as FileInfo).LastAccessTime}");
            else if (file is DirectoryInfo)
                Console.Write($"{"Folder",+36} {(file as DirectoryInfo).LastWriteTime}");
            else if (file is FolderUp)
                Console.Write($"{"UP",+36}");
        }

        public static void DrawPathes(ESection esection, string path1, string path2)
        {
            // путь к файлу слева
            Console.SetCursorPosition(2, 3);

            if (esection == ESection.Left)
                Console.BackgroundColor = Config.AdditionalBackgroundColor;
            Console.Write($"Current path: {GetNormalString(path1, (Config.HalfWindowWidth) - 19)}");
            Console.BackgroundColor = Config.BackgroundColor;


            // справва
            Console.SetCursorPosition((Config.HalfWindowWidth) + 2, 3);

            if (esection == ESection.Right)
                Console.BackgroundColor = Config.AdditionalBackgroundColor;
            Console.Write($"Current path: {GetNormalString(path2, (Config.HalfWindowWidth) - 19)}");
            Console.BackgroundColor = Config.BackgroundColor;
        }

        public static string CreateRect(string str, int length)
        {
            string str2 = " " + str;
            for (int i = str.Length; i < length; i++)
                str2 += " ";

            if (str2.Length > length)
                str2 = str2.Substring(0, length + 1);

            return str2;
        }

        public static string GetNormalString(string str, int length)
        {
            for (int i = str.Length; i < length; i++)
                str += " ";

            if (str.Length > length)
                str = str.Substring(0, length);

            return str;
        }

        public static void CleanScreen(int X, int Y)
        {
            Console.SetCursorPosition(X, Y);
            for (int i = 0; Console.CursorTop < Console.WindowHeight - 4; i++)
            {
                Console.Write(GetNormalString("", Config.QuarterWindowWidth - 1));
                Console.SetCursorPosition(X, ++Console.CursorTop);
            }

            Console.SetCursorPosition(X + Config.QuarterWindowWidth, Y);
            for (int i = 0; Console.CursorTop < Console.WindowHeight - 4; i++)
            {
                Console.Write(GetNormalString("", Config.QuarterWindowWidth - 1));
                Console.SetCursorPosition(X + Config.QuarterWindowWidth, ++Console.CursorTop);
            }
        }

        public static void DrawFiles(ArrayList Files, int X, int Y, int startIndex, int count)
        {
            Console.SetCursorPosition(X, Y);
            int counter = 0;
            for (int i = startIndex; i < Files.Count; i++)
            {
                if (counter++ >= count)
                    break;

                if (Console.CursorTop == Console.WindowHeight - 4)
                {
                    X += Config.QuarterWindowWidth;
                    Console.SetCursorPosition(X, Y);
                }

                if (Files[i] is DirectoryInfo)
                {
                    if ((Files[i] as DirectoryInfo).Name.Length > Config.QuarterWindowWidth - 2)
                        Console.Write(" " + (Files[i] as DirectoryInfo).Name.Substring(0, Config.QuarterWindowWidth - 2));
                    else
                        Console.Write(" " + (Files[i] as DirectoryInfo).Name);
                }
                else if (Files[i] is FileInfo)
                {
                    if ((Files[i] as FileInfo).Name.Length > Config.QuarterWindowWidth - 2)
                        Console.Write(" " + (Files[i] as FileInfo).Name.Substring(0, Config.QuarterWindowWidth - 2));
                    else
                        Console.Write(" " + (Files[i] as FileInfo).Name);
                }
                else if (Files[i] is FolderUp)
                {
                    Console.Write(" " + (Files[i] as FolderUp).Name);
                }

                Console.SetCursorPosition(X, ++Console.CursorTop);
            }
        }
    }
}
