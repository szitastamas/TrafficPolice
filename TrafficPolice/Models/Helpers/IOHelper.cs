using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using TrafficPolice.Models.Helpers;
using System.Text.RegularExpressions;

namespace TrafficPolice.Models
{
    public static class IOHelper
    {
        /// <summary>
        /// This function returns a boolean that indicates if the inserted location is valid. The location is valid if it starts with -Drive-:\\ which is an available drive (C, D, A etc.), has a filename and its extension is .txt
        /// </summary>
        /// <param name="location"></param>
        /// <returns>Boolean</returns>
        public static bool IsLocationValid(string location)
        {
            if(location == "")
            {
                return true;
            }

            int firstIndexOfSlash = location.IndexOf('\\');

            if (firstIndexOfSlash == -1 || firstIndexOfSlash < 2)
            {
                MessageHelper.PrintMessage("Please insert a valid location.", "danger");
                return false;
            }

            FileInfo file = new FileInfo(location);
            DriveInfo drive = new DriveInfo(file.ToString().Substring(0, firstIndexOfSlash));

            if (drive.IsReady == false)
            {
                MessageHelper.PrintMessage("Drive is not available. Please choose another one.", "danger");
                return false;
            }

            if (file.Extension != ".txt")
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
