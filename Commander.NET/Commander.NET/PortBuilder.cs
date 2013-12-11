using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public static class PortBuilder
    {
        public static Port buildPort(int id)
        {
            return PortBuilder.buildPort(id, null, "");
        }

        public static Port buildPort(int id, List<Vlan> vlans)
        {
            return PortBuilder.buildPort(id, vlans, "");
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
    }
}
