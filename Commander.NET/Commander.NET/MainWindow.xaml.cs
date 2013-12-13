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
using Microsoft.Win32;

namespace Commander.NET
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static List<Vlan> VLANS;
        public static Configuration CONFIG;

        public MainWindow()
        {
            CONFIG = new Configuration();
            CONFIG.Vlans.Add(VlanFactory.createVlan("WAN", "0", "#FFC000"));
            CONFIG.Vlans.Add(VlanFactory.createVlan("Default", "1", "#92D050"));
            CONFIG.Vlans.Add(VlanFactory.createVlan("Internal", "2", "#00B0F0"));
            CONFIG.Vlans.Add(VlanFactory.createVlan("External", "3", "brown"));
            CONFIG.Vlans.Add(VlanFactory.createVlan("DMZ", "4", "#FF0000"));
            CONFIG.Vlans.Add(VlanFactory.createVlan("Management", "254", "#E26B0A"));

            InitializeComponent();

            // Juniper EX3200-24T :-)
            for (int i = 0; i < 24; i++)
            {
                switchPanel.Children.Add(new SwitchPort(i));
            }
        }

        private void openConfig_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML config (.xml)|*.xml";
            bool? result = dlg.ShowDialog();

            if (result == true)
            {
                string filename = dlg.FileName;
            }
        }

        private void saveConfig_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = "Untitled";
            dlg.DefaultExt = ".xml";
            dlg.Filter = "XML config (.xml)|*.xml";
            bool? result = dlg.ShowDialog();

            if (result != true)
            {
                return;
            }
            
            ConfigurationFactory.saveConfigurationToFile(dlg.FileName, MainWindow.CONFIG);
        }
    }
}
