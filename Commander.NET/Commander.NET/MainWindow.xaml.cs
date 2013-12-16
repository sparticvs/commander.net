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
            CONFIG = ConfigurationFactory.createDefaultConfiguration();

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

            if (result != true)
            {
                return;
            }

            MainWindow.CONFIG = ConfigurationFactory.createConfigurationFromFile(dlg.FileName);
            this.loadConfig();
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

        private void loadConfig()
        {
            foreach (SwitchPort sp in switchPanel.Children)
            {
                sp.UpdatePortInfo(CONFIG.Ports[sp.PortInfo.Id]);
            }
        }
    }
}
