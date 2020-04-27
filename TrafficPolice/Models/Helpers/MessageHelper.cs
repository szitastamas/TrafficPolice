using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPolice.Models.Helpers
{
    static class MessageHelper
    {
        public static void PrintMessage(string text, string msgType = "default")
        {
            switch (msgType)
            {
                case "danger":
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case "warning":
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case "success":
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
            }

            Console.WriteLine(text);
            Console.ForegroundColor = ConsoleColor.White;

        }
    }
}
