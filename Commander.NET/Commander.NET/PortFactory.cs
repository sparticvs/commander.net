using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public static class PortFactory
    {
        public static Port buildPort(int id)
        {
            return PortFactory.buildPort(id, null, "");
        }

        public static Port buildPort(int id, List<Vlan> vlans)
        {
            return PortFactory.buildPort(id, vlans, "");
        }

        public static Port buildPort(int id, List<Vlan> vlans, string detail)
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

        public static Port buildPort(string sId, string sDetail)
        {
            int id = int.Parse(sId);
            return PortFactory.buildPort(id, null, sDetail);
        }
    }
}
