using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPolice.Models.Helpers
{
    static class MessageHelper
    {
        /// <summary>
        /// A centralized way to display console messages. The message type decides which color the message should have. If no message type provided, the default will be white. The color change has an affect only on the provided message as the color will be reset to white after each display.
        /// </summary>
        /// <param name="text">A string to be displayed</param>
        /// <param name="msgType">
        /// | default - white | 
        /// danger - red | 
        /// warning - yellow | 
        /// success - dark green | 
        /// </param>
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
