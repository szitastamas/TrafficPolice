using System;
using System.IO;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PcapDotNet.Core;
using PcapDotNet.Packets;
using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.IpV4;
using PcapDotNet.Packets.Transport;

namespace TrafficPolice.Models
{
    class Police
    {
        public IList<LivePacketDevice> Devices { get; set; } = LivePacketDevice.AllLocalMachine;
        QueryRepository repo = new QueryRepository();

        public void ChooseDevice()
        {
            int deviceCounter = 1;
            foreach (var dev in Devices)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("OPTION - {0} -", deviceCounter);
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(dev.Name);
                if (dev.Description != null)
                {
                    Console.WriteLine(dev.Description);
                }
                foreach (DeviceAddress address in dev.Addresses)
                {
                    if (!address.Address.ToString().StartsWith("Internet6"))
                    {

                        Console.WriteLine("Address: " + address.Address);
                        if (address.Destination != null)
                        {
                            Console.WriteLine("Destination: " + address.Destination);
                        }
                        Console.WriteLine(address.Netmask);
                    }
                }
                Console.WriteLine();
                deviceCounter++;
            }
            int deviceIndex = 0;
            do
            {
                Console.WriteLine("Enter the interface number (1-" + Devices.Count + "):");
                string deviceIndexString = Console.ReadLine();
                if (!int.TryParse(deviceIndexString, out deviceIndex) ||
                    deviceIndex < 1 || deviceIndex > Devices.Count)
                {
                    deviceIndex = 0;
                }
            } while (deviceIndex == 0);

            // Take the selected adapter
            PacketDevice selectedDevice = Devices[deviceIndex - 1];

            OpenDevice(selectedDevice);
        }

        private void OpenDevice(PacketDevice selectedDevice)
        {
            // Open the device
            using (PacketCommunicator communicator =
                selectedDevice.Open(65536,
                                    PacketDeviceOpenAttributes.MaximumResponsiveness,
                                    1000))
            {
                if (communicator.DataLink.Kind != DataLinkKind.Ethernet)
                {
                    Console.WriteLine("This program works only on Ethernet networks.");
                    return;
                }

                // Compile the filter
                using (BerkeleyPacketFilter filter = communicator.CreateFilter("ip and udp"))
                {
                    // Set the filter
                    communicator.SetFilter(filter);
                }

                Console.WriteLine("Listening on " + selectedDevice.Description + "...");

                // start the capture
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Monitoring started. Press any key to stop and save the results into a text file");
                Console.ForegroundColor = ConsoleColor.White;

                Packet packet;
                do
                {
                    PacketCommunicatorReceiveResult result = communicator.ReceivePacket(out packet);
                    switch (result)
                    {
                        case PacketCommunicatorReceiveResult.Timeout:
                            // Timeout elapsed
                            continue;
                        case PacketCommunicatorReceiveResult.Ok:
                            PacketHandler(packet);
                            break;
                        default:
                            throw new InvalidOperationException("The result " + result + " should never be reached here");
                    }
                } while (!Console.KeyAvailable);

                Console.WriteLine("Saving results....");
                repo.SaveMonitoringResultToTextFile();
                
            }
        }

        private void PacketHandler(Packet packet)
        {

            IpV4Datagram ip = packet.Ethernet.IpV4;
            DnsDatagram dns = ip.Udp.Dns;
            UdpDatagram udp = ip.Udp;
            IEnumerable<DnsResourceRecord> queries = packet.Ethernet.IpV4.Udp.Dns.ResourceRecords;

            if (queries != null || queries.ToList().Count > 0)
            {
                if (dns.IsQuery)
                {
                    Query query = new Query(dns.Id, ip.Source, ip.Destination, ip.Protocol, queries);
                    repo.Events.Add(query);
                    Console.WriteLine(query.PrintOutQueryInfo());
                }
                else if (dns.IsResponse)
                {
                    Response res = new Response(dns.Id, dns.ResponseCode, dns.DataResourceRecords, ip.Source, ip.Destination, ip.Protocol, queries);
                    repo.Events.Add(res);
                    Console.WriteLine(res.PrintOutQueryInfo());
                }
            }
        }
    }
}
