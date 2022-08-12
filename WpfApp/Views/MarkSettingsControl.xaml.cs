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
using TL_Tables.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MarkSettingsControl.xaml
    /// </summary>
    public partial class MarkSettingsControl : UserControl
    {
        private readonly IHasMarkSystem table;
        private readonly IHasMarkSystem categoryTable;
        public MarkSettingsControl(IHasMarkSystem table, IHasMarkSystem categoryTable)
        {
            this.table = table;
            this.categoryTable = categoryTable;
            InitializeComponent();
            markPanel.Items.Add(3);
            markPanel.Items.Add(5);
            markPanel.Items.Add(6);
            markPanel.Items.Add(10);
            markPanel.Items.Add(12);
            markPanel.Items.Add(25);
            markPanel.SelectedItem = table.MarkSystem;
        }

        private void ChangeMark(object sender, RoutedEventArgs e)
        {
            int ms = (int)markPanel.SelectedItem;
            table.MarkSystem = ms;
            categoryTable.MarkSystem = ms;
        }
    }
}
