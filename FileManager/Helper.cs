using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    public static class Helper
    {
        public static double Length(this DirectoryInfo dir)
        {
            double l = 0;

            return 1;
        }

        public static bool IsCorrect(ConsoleKeyInfo c)
        {
            switch (c.Key)
            {              
                case ConsoleKey.A:
                case ConsoleKey.B:
                case ConsoleKey.C:
                case ConsoleKey.D:
                case ConsoleKey.E:
                case ConsoleKey.F:
                case ConsoleKey.G:
                case ConsoleKey.H:
                case ConsoleKey.I:
                case ConsoleKey.J:
                case ConsoleKey.K:
                case ConsoleKey.L:
                case ConsoleKey.M:
                case ConsoleKey.N:
                case ConsoleKey.O:
                case ConsoleKey.P:
                case ConsoleKey.Q:
                case ConsoleKey.R:
                case ConsoleKey.S:
                case ConsoleKey.T:
                case ConsoleKey.U:
                case ConsoleKey.V:
                case ConsoleKey.W:
                case ConsoleKey.X:
                case ConsoleKey.Y:
                case ConsoleKey.Z:
                case ConsoleKey.D0:
                case ConsoleKey.D1:
                case ConsoleKey.D2:
                case ConsoleKey.D3:
                case ConsoleKey.D4:
                case ConsoleKey.D5:
                case ConsoleKey.D6:
                case ConsoleKey.D7:
                case ConsoleKey.D8:
                case ConsoleKey.D9:
                case ConsoleKey.Spacebar:
                case ConsoleKey.Decimal:
                case ConsoleKey.OemComma:
                case ConsoleKey.OemMinus:
                case ConsoleKey.OemPeriod:
                case ConsoleKey.OemPlus:
                    return true;
                default:
                    return false;
            }
        }
    }
}
