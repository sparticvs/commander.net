using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Commander.NET
{
    public class VlanSelectedEventArgs : EventArgs
    {
        public Port Port { get; set; }

        public VlanSelectedEventArgs()
        {
        }
    }
}
