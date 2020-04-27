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
using TrafficPolice.Models.Helpers;

namespace TrafficPolice
{
    class Program
    {
        static void Main(string[] args)
        {
            // Welcoming user with basic instructions
            WelcomeUser();
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

        private static void WelcomeUser()
        {
            MessageHelper.PrintMessage("Welcome to the Traffic Police application!");
            MessageHelper.PrintMessage(@"Type in a location to save the result of the monitoring. The default location is C:\\DNS_Protocol.txt");
            MessageHelper.PrintMessage("Note: saving the protocol file in the default location requires administrator rights.", "warning");
        }
    }
}
