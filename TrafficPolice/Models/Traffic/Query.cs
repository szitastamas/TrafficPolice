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
    class Query : NetworkEvent
    {
        public Query(ushort id, IpV4Address source, IpV4Address target, IpV4Protocol protocol, IEnumerable<DnsResourceRecord> queries) : base(id, source, target, protocol, queries)
        {
        }
    }
}
