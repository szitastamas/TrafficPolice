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
    class Response : NetworkEvent
    {
        public ushort Id { get; set; }
        public IEnumerable<DnsDataResourceRecord> ResponseData { get; set; }
        public DnsResponseCode ResponseCode { get; set; }
        public Response(ushort id, DnsResponseCode responseCode, IEnumerable<DnsDataResourceRecord> resData, IpV4Address source, IpV4Address target, IpV4Protocol protocol, IEnumerable<DnsResourceRecord> queries) : base(id, source, target, protocol, queries)
        {
            ResponseCode = responseCode;
            ResponseData = resData;
        }

        public override string PrintOutQueryInfo()
        {
            return $"Response Code: {ResponseCode}\n{base.PrintOutQueryInfo()}";
        }
    }
}
