﻿using System;
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
        public SwitchPort(int portId)
        {
            InitializeComponent();
            this.portId.Text = portId.ToString();
        }

        private void Rectangle_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            VlanSelector selector = new VlanSelector();
            selector.SetVlans(MainWindow.VLANS);
            selector.OnSave += this.HandleSaveEvent;
            selector.Show();
        }

        private void Grid_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            e.Handled = true;
        }

        private void HandleSaveEvent(object sender, VlanSelectedEventArgs e)
        {
            if (e.Vlans.Count > 0)
            {
                this.portRect.Fill = new SolidColorBrush(e.Vlans[0].Color);
                this.portRect.ToolTip = e.Vlans[0].Name;
            }
            else
            {
                this.portRect.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFDEDEDE"));
            }
        }
    }
}
