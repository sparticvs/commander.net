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
    /// Interaction logic for SwitchPort.xaml
    /// </summary>
    public partial class SwitchPort : UserControl
    {
        private Port portInfo;

        public Port PortInfo { get { return this.portInfo; } }

        public SwitchPort(int portId)
        {
            this.portInfo = PortFactory.createPort(portId);
            InitializeComponent();
            this.portId.Text = this.portInfo.Id.ToString();
        }

        private void Rectangle_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            VlanSelector selector = new VlanSelector(this.portInfo);
            selector.SetVlans(MainWindow.CONFIG.Vlans);
            selector.OnSave += this.HandleSaveEvent;
            selector.Show();
        }

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            // Don't show context menu when right click on the port. That has a different action
            e.Handled = true;
        }

        private void HandleSaveEvent(object sender, VlanSelectedEventArgs e)
        {
            UpdateConfiguration(e.Port);
            this.UpdatePortInfo(e.Port);
        }

        /* TODO: Move this to Configuration */
        public static void UpdateConfiguration(Port port)
        {
            foreach (Port p in MainWindow.CONFIG.Ports)
            {
                if (port.Id == p.Id)
                {
                    p.Details = port.Details;
                    p.Vlans = port.Vlans;
                }
            }
        }

        public void UpdatePortInfo(Port port)
        {
            this.portInfo = port;

            if (this.portInfo.Vlans.Count > 0)
            {
                this.portRect.Fill = new SolidColorBrush(port.Vlans[0].Color);
                this.portRect.ToolTip = port.Vlans[0].Name;
            }
            else
            {
                this.portRect.Fill = new SolidColorBrush(MainWindow.CONFIG.OpenPortColor);
            }
        }
    }
}
