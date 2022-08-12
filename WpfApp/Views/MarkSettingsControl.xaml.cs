using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TL_Tables;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MarkSettingsControl.xaml
    /// </summary>
    public partial class MarkSettingsControl : UserControl
    {
        private readonly FilmsTable table;
        public MarkSettingsControl(FilmsTable table)
        {
            this.table = table;
            InitializeComponent();
            markPanel.Items.Add(3);
            markPanel.Items.Add(5);
            markPanel.Items.Add(6);
            markPanel.Items.Add(10);
            markPanel.Items.Add(12);
            markPanel.SelectedItem = table.MarkSystem;
        }

        private void ChangeMark(object sender, RoutedEventArgs e)
        {
            table.MarkSystem = (int)markPanel.SelectedItem;
        }
    }
}
