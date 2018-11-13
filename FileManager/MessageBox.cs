using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public abstract class MessageBox
    {
        public string Action { get; set; }
        public string Text { get; set; }

        protected int w;
        protected int h;

        protected int msgbWidth;
        protected int msgbHeigth;

        protected int x;
        protected int y;

        public MessageBox(string action, string text)
        {
            Action = action;
            Text = text;

            // -------------------------------------

            w = Config.WindowWidth;
            h = Config.WindowHeight;

            msgbWidth = (int)(w / 1.8);
            msgbHeigth = (int)(h / 2.5);

            x = (w / 2) - (int)(msgbWidth / 2);
            y = (h / 2) - (int)(msgbHeigth / 2);
        }

        public void GetMessageBox()
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = Config.MsgBoxBackgroundColor;

            string buf = "";
            for (int i = 0; i < msgbHeigth; i++)
            {
                buf = "";
                for (int j = 0; j < msgbWidth; j++)
                {
                    buf += " ";
                }
                Console.Write(buf);
                Console.SetCursorPosition(x, ++Console.CursorTop);
            }

            Console.ForegroundColor = Config.MsgBoxForegroundColor;
            Console.SetCursorPosition(x + 3, y + 1);

            buf = "┌";
            for (int i = 0; i < msgbWidth - 8; i++)
            {
                buf += "─";
            }
            buf += "┐";
            Console.Write(buf);

            buf = "";
            Console.SetCursorPosition(x + 3, y + 2);
            for (int j = 0; j < msgbHeigth - 4; j++)
            {
                buf = "│";
                for (int i = 0; i < msgbWidth - 8; i++)
                {
                    buf += " ";
                }
                buf += "│";
                Console.Write(buf);

                Console.SetCursorPosition(x + 3, ++Console.CursorTop);
            }

            buf = "└";
            for (int i = 0; i < msgbWidth - 8; i++)
            {
                buf += "─";
            }
            buf += "┘";
            Console.WriteLine(buf);

            // -----------------------------------------------------

            Console.SetCursorPosition(x + 3 + ((msgbWidth - 6) / 2) - (Action.Length / 2), y + 2);
            Console.Write(Action);

            Console.SetCursorPosition(x + 3 + ((msgbWidth - 6) / 2) - (Text.Length / 2), y + 4);
            Console.Write(Text);

            Console.SetCursorPosition(x + 4, y + 6);
            buf = "";
            for (int i = 0; i < msgbWidth - 8; i++)
            {
                buf += "─";
            }
            Console.WriteLine(buf);

            // -----------------------------------------------------
        }
    }

    public class YesNoMessageBox : MessageBox
    {
        public YesNoMessageBox(string action, string text) : base(action, text) { }

        public new bool GetMessageBox()
        {
            base.GetMessageBox();
            bool selectedOK = true;

            while (true)
            {
                Console.SetCursorPosition(x + ((msgbWidth - 6) / 2) - 3, y + msgbHeigth - 3);

                if (selectedOK)
                {
                    Console.BackgroundColor = Config.AdditionalMsgBoxBackgroundColor;
                    Console.Write("OK");
                    Console.BackgroundColor = Config.MsgBoxBackgroundColor;
                }
                else
                    Console.Write("OK");

                Console.Write("      ");

                if (!selectedOK)
                {
                    Console.BackgroundColor = Config.AdditionalMsgBoxBackgroundColor;
                    Console.Write("CANCEL");
                    Console.BackgroundColor = Config.MsgBoxBackgroundColor;
                }
                else
                    Console.Write("CANCEL");

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        Console.BackgroundColor = Config.BackgroundColor;
                        Console.ForegroundColor = Config.ForegroundColor;

                        if (selectedOK)
                            return true;
                        return false;
                    case ConsoleKey.LeftArrow:
                        selectedOK = true;
                        break;
                    case ConsoleKey.RightArrow:
                        selectedOK = false;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    public class OkMessageBox : MessageBox
    {
        public OkMessageBox(string action, string text) : base(action, text) { }

        public new bool GetMessageBox()
        {
            base.GetMessageBox();

            while (true)
            {
                Console.SetCursorPosition(x + ((msgbWidth) / 2), y + msgbHeigth - 3);
                Console.BackgroundColor = Config.AdditionalMsgBoxBackgroundColor;
                Console.Write("OK");
                Console.BackgroundColor = Config.MsgBoxBackgroundColor;

                switch (Console.ReadKey(true).Key)
                {
                    case ConsoleKey.Enter:
                        Console.BackgroundColor = Config.BackgroundColor;
                        Console.ForegroundColor = Config.ForegroundColor;
                        return true;
                    default:
                        break;
                }
            }
        }
    }
}
