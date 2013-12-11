using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Commander.NET
{
    public class Vlan
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Color Color { get; set; }

        public Vlan()
        {
        }

        public override string ToString()
        {
            return string.Format("{0} :: VLAN{1}", Name, Id);
        }
    }
}
