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

        public void SaveMonitoringResultToTextFile()
        {
            StreamWriter writer = new StreamWriter(@"A:\\Protocol.txt", true);

            Events.ForEach(e =>
            {
                try
                {
                    writer.WriteLine(e.PrintOutQueryInfo());
                }
                catch (Exception err)
                {
                    Console.WriteLine(err.Message);
                    throw;
                }
            });

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("Results successfully saved");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Press a key to close the program");
            Console.ReadKey();
        }
    }
}
