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
            VLANS.Add(VlanFactory.createVlan("Open", "-1", "gray"));
            VLANS.Add(VlanFactory.createVlan("WAN", "0", "yellow"));
            VLANS.Add(VlanFactory.createVlan("Default", "1", "orange"));
            VLANS.Add(VlanFactory.createVlan("Internal", "2", "green"));
            VLANS.Add(VlanFactory.createVlan("External", "3", "blue"));
            VLANS.Add(VlanFactory.createVlan("DMZ", "4", "red"));
            VLANS.Add(VlanFactory.createVlan("Management", "254", "purple"));

            InitializeComponent();

            // Juniper EX3200-24T :-)
            for (int i = 0; i < 24; i++)
            {
                switchPanel.Children.Add(new SwitchPort(i));
            }
        }
    }
}
