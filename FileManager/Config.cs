using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public static class Config
    {
        // Can be changed
        public static ConsoleColor  BackgroundColor                 = ConsoleColor.Black;
        public static ConsoleColor  ForegroundColor                 = ConsoleColor.White;
        public static ConsoleColor  AdditionalBackgroundColor       = ConsoleColor.Red;
        public static ConsoleColor  MsgBoxBackgroundColor           = ConsoleColor.White;
        public static ConsoleColor  MsgBoxForegroundColor           = ConsoleColor.Black;
        public static ConsoleColor  AdditionalMsgBoxBackgroundColor = ConsoleColor.Red;
        public static ConsoleColor  TextBoxBackgroundColor          = ConsoleColor.Gray;
        public static string        LeftSectionPath                 = @"C:\Users\Valeriy\Desktop\";
        public static string        RightSectionPath                = @"C:\Users\Valeriy\Desktop\";
        public static int           StartupWindowWidth              = /*Console.LargestWindowWidth;*/120; // Default - 120
        public static int           StartupWindowHeight             = /*Console.LargestWindowHeight;*/30; // Default - 30

        // Can not be changed
        public static int           WindowWidth                     = StartupWindowWidth - 3;
        public static int           QuarterWindowWidth              = WindowWidth / 4;
        public static int           HalfWindowWidth                 = WindowWidth / 2;
        public static int           WindowHeight                    = StartupWindowHeight - 3;
        public static int           FilesCountOneSection            = StartupWindowHeight - 11;
        public static int           FilesCountTwoSections           = FilesCountOneSection * 2;
    }
}
