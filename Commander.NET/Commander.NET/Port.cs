using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public class Port
    {
        public int Id { get; set; }
        public List<Vlan> Vlans { get; set; }
        public string Details { get; set; }

        public Port()
        {
            this.Vlans = new List<Vlan>();
        }
    }
}
