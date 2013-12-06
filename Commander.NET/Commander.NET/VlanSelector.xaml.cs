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
        public EventHandler OnSave { get; set; }

        public VlanSelector()
        {
            InitializeComponent();
        }

        public void SetVlans(List<Vlan> vlans)
        {
            foreach (Vlan v in vlans)
            {
                this.vlanLB.Items.Add(string.Format("{0} :: VLAN{1}", v.Name, v.Id));
            }
        }

        private void saveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.OnSave != null)
            {
                this.OnSave(this, new EventArgs());
            }
        }
    }
}
