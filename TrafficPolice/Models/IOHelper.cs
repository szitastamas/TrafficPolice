using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TrafficPolice.Models
{
    public static class IOHelper
    {
        public static bool IsLocationValid(string location)
        {
            if(location == "")
            {
                return true;
            }

            FileInfo file = new FileInfo(location);
            int firstIndexOfSlash = file.ToString().IndexOf('\\');

            if (firstIndexOfSlash == -1)
            {
                DisplayErrorMessage("Please insert a valid location.");
                return false;
            }

            DriveInfo drive = new DriveInfo(file.ToString().Substring(0, firstIndexOfSlash));

            if (drive.IsReady == false)
            {
                DisplayErrorMessage("Drive is not available. Please choose another one.");
                return false;
            }

            if(file.Extension != ".txt")
            {
                DisplayErrorMessage("File extension is invalid. It must be .txt - Please try again.");
                return false;
            }

            if (file.Directory.Exists == false)
            {
                Directory.CreateDirectory(file.Directory.FullName);
            }

            return true;
        }

        private static void DisplayErrorMessage(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
