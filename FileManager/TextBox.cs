using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class TextBox : MessageBox
    {
        public TextField Field { get; private set; }

        public TextBox(string action, string text) : base(action, text)
        {
            Field = new TextField(msgbWidth - 20);
        }

        public new string GetMessageBox()
        {
            base.GetMessageBox();

            Console.SetCursorPosition(x + ((msgbWidth) / 2), y + msgbHeigth - 3);
            Console.BackgroundColor = Config.AdditionalMsgBoxBackgroundColor;
            Console.Write("OK");
            Console.BackgroundColor = Config.MsgBoxBackgroundColor;

            try { Field.GetTextField(x + 10, y + 5); }
            catch (Exception) { throw; }
            finally
            {
                Console.BackgroundColor = Config.BackgroundColor;
                Console.ForegroundColor = Config.ForegroundColor;
            }

            return Field.Text;
        }
    }
}
