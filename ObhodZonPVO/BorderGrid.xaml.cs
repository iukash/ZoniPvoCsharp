using System;
using System.Collections.Generic;
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

namespace ObhodZonPVO
{
    /// <summary>
    /// Interaction logic for BorderGrid.xaml
    /// </summary>
    public partial class BorderGrid : UserControl
    {
        public BorderGrid()
        {
            InitializeComponent();
        }

        public void SetContent(string str)
        {
            Dispatcher.BeginInvoke((Action)(() => { Lbl.Content = str; }));
        }
    }
}
