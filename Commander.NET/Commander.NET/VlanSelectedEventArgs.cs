using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public class VlanSelectedEventArgs : EventArgs
    {
        public List<Vlan> Vlans { get; set; }

        public VlanSelectedEventArgs()
        {
            Vlans = new List<Vlan>();
        }
    }
}
