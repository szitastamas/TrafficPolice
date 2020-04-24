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
            Police police = new Police();
            police.ChooseDevice();

            Console.ReadKey();
        }
    }
}
