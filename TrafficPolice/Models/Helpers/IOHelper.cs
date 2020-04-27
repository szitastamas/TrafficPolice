using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TrafficPolice.Models.Helpers;

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
                MessageHelper.PrintMessage("Please insert a valid location.", "danger");
                return false;
            }

            DriveInfo drive = new DriveInfo(file.ToString().Substring(0, firstIndexOfSlash));

            if (drive.IsReady == false)
            {
                MessageHelper.PrintMessage("Drive is not available. Please choose another one.", "danger");
                return false;
            }

            if(file.Extension != ".txt")
            {
                MessageHelper.PrintMessage("File extension is invalid. It must be .txt - Please try again.", "danger");
                return false;
            }

            if (file.Directory.Exists == false)
            {
                Directory.CreateDirectory(file.Directory.FullName);
            }

            return true;
        }
    }
}
