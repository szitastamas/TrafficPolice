using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                Console.WriteLine("Saving results....");

                Events.ForEach(e =>
                {
                    try
                    {
                        writer.WriteLine(e.PrintOutQueryInfo());
                    }
                    catch (Exception exc)
                    {
                        Console.WriteLine(exc.Message);
                        throw;
                    }
                });

                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("Results successfully saved");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Press a key to close the program");
                Console.ReadKey();
            }
            catch (Exception exc)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                if (exc is UnauthorizedAccessException)
                {
                    Console.WriteLine("The application is not authorized to save files to the provided location. Please start the application in administrator mode and try again.");
                }
                else
                {
                    Console.WriteLine(exc.Message);
                }
                Console.ReadKey();
            }
        }
    }
}
