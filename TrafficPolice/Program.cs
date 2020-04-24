using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrafficPolice.Models;

namespace TrafficPolice
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Traffic Police application!");
            Console.WriteLine(@"Type in a location to save the result of the monitoring. The default location is C:\\Protocol.txt");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Note: saving the protocol file here requires administrator rights.");
            Console.ForegroundColor = ConsoleColor.White;
            string location = "Invalid Location";

            do
            {
                Console.Write("Type in the desired location: ");
                location = Console.ReadLine();

            } while (IOHelper.IsLocationValid(location) == false);

            Police police = new Police(location);
            police.ChooseDevice();

            Console.ReadKey();
        }
    }
}
