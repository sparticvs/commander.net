using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Media;

namespace Commander.NET
{
    public static class VlanFactory
    {
        public static Vlan createVlan(string name, string id, string color)
        {
            Vlan vlan = new Vlan();

            vlan.Name = name;
            vlan.Id = int.Parse(id);
            vlan.Color = (Color)ColorConverter.ConvertFromString(color);

            return vlan;
        }
    }
}
