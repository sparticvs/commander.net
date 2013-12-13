using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public static class PortFactory
    {
        public static Port createPort(int id)
        {
            return PortFactory.createPort(id, null, "");
        }

        public static Port createPort(int id, List<Vlan> vlans)
        {
            return PortFactory.createPort(id, vlans, "");
        }

        public static Port createPort(int id, List<Vlan> vlans, string detail)
        {
            Port port = new Port();
            port.Details = detail;
            port.Id = id;
            port.Vlans.Clear();
            if (vlans != null)
            {
                port.Vlans.AddRange(vlans);
            }
            return port;
        }

        public static Port createPort(string sId, string sDetail)
        {
            int id = int.Parse(sId);
            return PortFactory.createPort(id, null, sDetail);
        }
    }
}
