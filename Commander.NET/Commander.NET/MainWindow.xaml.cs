using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Commander.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Vlan> VLANS;

        public MainWindow()
        {
            VLANS = new List<Vlan>();
            VLANS.Add(VlanFactory.createVlan("WAN", "0", "#FFC000"));
            VLANS.Add(VlanFactory.createVlan("Default", "1", "#92D050"));
            VLANS.Add(VlanFactory.createVlan("Internal", "2", "#00B0F0"));
            VLANS.Add(VlanFactory.createVlan("External", "3", "brown"));
            VLANS.Add(VlanFactory.createVlan("DMZ", "4", "#FF0000"));
            VLANS.Add(VlanFactory.createVlan("Management", "254", "#E26B0A"));

            InitializeComponent();

            // Juniper EX3200-24T :-)
            for (int i = 0; i < 24; i++)
            {
                switchPanel.Children.Add(new SwitchPort(i));
            }
        }
    }
}
