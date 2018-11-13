using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class TextField
    {
        public string Text { get; private set; }
        public int Length { get; set; }

        public TextField(int length)
        {
            Text = "";
            Length = length;
        }

        public void DrawTetField(int x, int y)
        {
            Console.SetCursorPosition(x, y);
            Console.BackgroundColor = Config.TextBoxBackgroundColor;

            string buf = "";
            for (int i = 0; i < Length; i++)
                buf += " ";

            Console.Write(buf);
        }

        public void GetTextField(int x, int y)
        {
            int _x = x;

            DrawTetField(x, y);

            Console.SetCursorPosition(x, y);
            Console.CursorVisible = true;

            var builder = new StringBuilder("");

            ConsoleKeyInfo symbol;
            while (true)
            {
                Console.SetCursorPosition(_x + builder.Length, y);
                symbol = Console.ReadKey(true);

                if (symbol.Key == ConsoleKey.Escape)
                {
                    Console.CursorVisible = false;
                    Console.BackgroundColor = Config.BackgroundColor;
                    throw new Exception("Finish action");
                }
                else if (symbol.Key == ConsoleKey.Enter)
                    break;
                else if (symbol.Key == ConsoleKey.Backspace)
                {
                    if (builder.Length == 0)
                        continue;

                    builder.Remove(builder.Length - 1, 1);
                    DrawTetField(x, y);

                    Console.SetCursorPosition(x, y);
                    Console.Write(builder);
                }
                else
                {
                    if (builder.Length == Length - 1)
                        continue;

                    if (Helper.IsCorrect(symbol))
                    {
                        builder.Append(symbol.KeyChar);
                        Console.Write(symbol.KeyChar);
                    }
                }
            }

            Text = builder.ToString();
            Console.CursorVisible = false;
            Console.BackgroundColor = Config.BackgroundColor;
        }
    }
}
