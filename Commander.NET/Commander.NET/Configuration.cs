using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Commander.NET
{
    public class Configuration
    {
        public string Name { get; set; }
        public string Serial { get; set; }
        public string Hostname { get; set; }
        public string OpenPortName { get; set; }
        public Color OpenPortColor { get; set; }
        public List<Vlan> Vlans { get; set; }
        public PortMap Ports { get; set; }

        public Configuration()
        {
            this.Vlans = new List<Vlan>();
            this.Ports = new PortMap();
        }
    }
}
