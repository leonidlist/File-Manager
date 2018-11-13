using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.IO;
using System.Diagnostics;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {
            FileManager manager = new FileManager("File Manager");

            GUI.DrawMenu(manager.FileManagerName);
            GUI.DrawName();
            GUI.DrawBottomPanel();
            Console.CursorVisible = false;

            manager.DisplayFilesFromTwoSections();
            YesNoMessageBox MsgBox = new YesNoMessageBox("none", "none");
            OkMessageBox InfMsgBox = new OkMessageBox("Information!", "none");
            TextBox TextBox = new TextBox("Please, write name", "");
            Searcher FileSearcher = new Searcher();

            while (true)
            {
                GUI.DrawPathes(manager.Esection, manager.LPath, manager.RPath);

                if (manager.Esection == ESection.Left)
                    manager.LeftSection.Cursor.UnmaskSelection();
                else
                    manager.RightSection.Cursor.UnmaskSelection();

                bool canReturn = false;
                while (!canReturn)
                {
                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.LeftArrow:
                            manager.MoveUp();
                            break;
                        case ConsoleKey.UpArrow:
                            manager.MoveUp();
                            break;
                        case ConsoleKey.RightArrow:
                            manager.MoveDown();
                            break;
                        case ConsoleKey.DownArrow:
                            manager.MoveDown();
                            break;
                        case ConsoleKey.Tab:
                            manager.ChangeSection();
                            break;
                        case ConsoleKey.Enter:
                            {
                                if (manager.GetCurrentFile() is FileInfo)
                                {
                                    Process.Start((manager.GetCurrentFile() as FileInfo).Name);
                                    break;
                                }
                                try
                                {
                                    manager.ChangeDirectory(manager.GetSelectedPath());
                                    manager.DisplayFiles();
                                }
                                catch (Exception ex)
                                {
                                    if (ex is UnauthorizedAccessException)
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "You don't have permissions enough.";
                                        InfMsgBox.GetMessageBox();
                                    }

                                    manager.ChangeDirectory(".");
                                    manager.Reload();
                                }
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.Backspace:
                            {
                                manager.ChangeDirectory("..");
                                manager.DisplayFiles();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F1:
                            {
                                InfMsgBox.Action = "Information";
                                try
                                { InfMsgBox.Text = manager.GetFileInfo(); }
                                catch (Exception) { break; }

                                InfMsgBox.GetMessageBox();

                                manager.Reload();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F2:
                            {
                                string fileName;
                                try
                                { fileName = TextBox.GetMessageBox(); }
                                catch (Exception)
                                {
                                    manager.Reload();
                                    canReturn = true;
                                    break;
                                }

                                try
                                { manager.CreateFile(fileName); }
                                catch (Exception ex)
                                {
                                    if (ex is UnauthorizedAccessException)
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "You don't have permissions enough.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                    else
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "Name contains incorrect symbols.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                }

                                manager.Reload();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F3:
                            {
                                string name;
                                try
                                { name = TextBox.GetMessageBox(); }
                                catch (Exception)
                                {
                                    manager.Reload();
                                    canReturn = true;
                                    break;
                                }

                                try
                                { manager.CreateFolder(name); }
                                catch (Exception ex)
                                {
                                    if (ex is UnauthorizedAccessException)
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "You don't have permissions enough.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                    else
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "Name contains incorrect symbols.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                }

                                manager.Reload();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F8:
                            {
                                MsgBox.Action = "Delete";
                                MsgBox.Text = "Do you really want to delete?";

                                if (MsgBox.GetMessageBox())
                                    manager.DeleteCurrentFile();

                                manager.Reload();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F6:
                            {
                                MsgBox.Action = "Move";
                                MsgBox.Text = "Do you really want to move?";

                                if (MsgBox.GetMessageBox())
                                    manager.MoveCurrentFile();

                                manager.Reload();
                                canReturn = true;
                            }
                            break;
                        case ConsoleKey.F7:
                            {
                                FileSearcher.Clear();
                                string search;
                                ArrayList searchRes = null;

                                try
                                { search = TextBox.GetMessageBox(); }
                                catch (Exception)
                                {
                                    manager.Reload();
                                    canReturn = true;
                                    break;
                                }

                                FileSearcher.Search(new DirectoryInfo(manager.GetCurrentPath()), search);
                                try
                                {
                                    searchRes = FileSearcher.GetResults();

                                    manager.Reload();
                                    manager.SetSearchResults(searchRes);
                                    manager.DisplayFiles();
                                }
                                catch (Exception ex)
                                {
                                    if (ex is NoResultException)
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "No results.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                    else
                                    {
                                        InfMsgBox.Action = "Error";
                                        InfMsgBox.Text = "Name contains incorrect symbols.";
                                        InfMsgBox.GetMessageBox();
                                    }
                                    manager.Reload();
                                }

                                canReturn = true;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
    }
}
