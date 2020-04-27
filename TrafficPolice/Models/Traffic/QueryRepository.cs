using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPolice.Models.Helpers;

namespace TrafficPolice.Models
{
    class QueryRepository
    {
       public List<NetworkEvent> Events { get; set; } = new List<NetworkEvent>();

        public void SaveMonitoringResultToTextFile(string fileLocation)
        {
            try
            {
                StreamWriter writer = new StreamWriter(fileLocation, true);
                MessageHelper.PrintMessage("Saving results....");

                Events.ForEach(e =>
                {
                    try
                    {
                        writer.WriteLine(e.PrintOutQueryInfo());
                    }
                    catch (Exception exc)
                    {
                        MessageHelper.PrintMessage(exc.Message, "danger");
                        throw;
                    }
                });

                MessageHelper.PrintMessage("Results successfully saved", "success");
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
