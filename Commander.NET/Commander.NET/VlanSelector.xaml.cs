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
using System.Windows.Shapes;

namespace Commander.NET
{
    /// <summary>
    /// Interaction logic for VlanSelector.xaml
    /// </summary>
    public partial class VlanSelector : Window
    {
        // TODO: Name of VlanSelection should change to "Port Info" or something relating to the port

        public EventHandler<VlanSelectedEventArgs> OnSave { get; set; }
        private Port portInfo;

        public VlanSelector(Port portInfo)
        {
            this.portInfo = portInfo;

            InitializeComponent();

            this.portDetails.Text = this.portInfo.Details;
            foreach (Vlan v in this.portInfo.Vlans)
            {
                this.vlanLB.SelectedItems.Add(v);
            }
        }

        public void SetVlans(List<Vlan> vlans)
        {
            foreach (Vlan v in vlans)
            {
                this.vlanLB.Items.Add(v);
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            this.portInfo.Details = this.portDetails.Text;

            if (this.OnSave != null)
            {
                VlanSelectedEventArgs args = new VlanSelectedEventArgs();
                this.portInfo.Vlans.Clear();
                foreach (Vlan v in this.vlanLB.SelectedItems)
                {
                    this.portInfo.Vlans.Add(v);
                }
                args.Port = this.portInfo;
                this.OnSave(this, args);
            }

            this.Close();
        }
    }
}
