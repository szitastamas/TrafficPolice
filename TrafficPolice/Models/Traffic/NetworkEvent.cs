using PcapDotNet.Packets.Dns;
using PcapDotNet.Packets.IpV4;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrafficPolice.Models
{
    abstract class NetworkEvent
    {
        public ushort Id { get; set; }
        public IpV4Address Source { get; set; }
        public IpV4Address Target { get; set; }
        public IpV4Protocol Protocol { get; set; }
        public IEnumerable<DnsResourceRecord> Queries { get; set; }
        public NetworkEvent(ushort id, IpV4Address source, IpV4Address target, IpV4Protocol protocol, IEnumerable<DnsResourceRecord> queries)
        {
            Id = id;
            Source = source;
            Target = target;
            Protocol = protocol;
            Queries = queries;
        }

        /// <summary>
        /// This overrideable method creates the string that will be displayed in the console during the monitoring phase and it will be saved in the protocol text file.
        /// </summary>
        /// <returns>String</returns>
        public virtual string PrintOutQueryInfo()
        {
            string displayMessage = "";
            displayMessage+= $"Event Type: {this.GetType().Name}\nEvent ID: {Id}\nSource Address: {Source}\nTarget Address: {Target}\nProtocol type: {Protocol}";

            foreach (var q in Queries)
            {
                displayMessage += $"\nDomain Name:{q.DomainName}";
                displayMessage += $"\nDNS Type:{q.DnsType}";
                displayMessage += $"\nDNS Class:{q.DnsClass}";
            }
            displayMessage += "\n";
            return displayMessage;
        }
    }
}
