﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPolice.Models.Helpers;

namespace TrafficPolice.Models
{
    /// <summary>
    /// A centralized store for all the DNS Queries and Responses that occured. Its method saves the event infos into the file that has been provided by the user.
    /// </summary>
    class EventRepository
    {
        public List<NetworkEvent> Events { get; set; } = new List<NetworkEvent>();

        public void SaveMonitoringResultsToTextFile(string fileLocation)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileLocation, true);
                MessageHelper.PrintMessage("Saving results....");

                foreach (NetworkEvent ev in Events)
                {
                    writer.WriteLine(ev.PrintOutQueryInfo());
                }

                MessageHelper.PrintMessage("Results successfully saved", "sucecss");
                MessageHelper.PrintMessage("Press a key to close the program");
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                if (exc is UnauthorizedAccessException)
                {
                    MessageHelper.PrintMessage("The application is not authorized to save files to the provided location. Please start the application in administrator mode and try again.", "danger");
                }
                else
                {
                    MessageHelper.PrintMessage(exc.Message, "danger");
                }
                Console.ReadKey();
            }
        }
    }
}
