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
            markPanel.Items.Add("3/3");
            markPanel.Items.Add("5/5");
            markPanel.Items.Add("6/6");
            markPanel.Items.Add("10/10");
            markPanel.Items.Add("12/12");
            markPanel.Items.Add("25/25");

            switch (table.MarkSystem)
            {
                case 3:
                    markPanel.SelectedIndex = 0;
                    break;
                case 5:
                    markPanel.SelectedIndex = 1;
                    break;
                case 6:
                    markPanel.SelectedIndex = 2;
                    break;
                case 10:
                    markPanel.SelectedIndex = 3;
                    break;
                case 12:
                    markPanel.SelectedIndex = 4;
                    break;
                case 25:
                    markPanel.SelectedIndex = 5;
                    break;
            }
        }

        private void ChangeMark(object sender, RoutedEventArgs e)
        {
            switch (markPanel.SelectedIndex)
            {
                case 0:
                    table.MarkSystem = 3;
                    categoryTable.MarkSystem = 3;
                    break;
                case 1:
                    table.MarkSystem = 5;
                    categoryTable.MarkSystem = 5;
                    break;
                case 2:
                    table.MarkSystem = 6;
                    categoryTable.MarkSystem = 6;
                    break;
                case 3:
                    table.MarkSystem = 10;
                    categoryTable.MarkSystem = 10;
                    break;
                case 4:
                    table.MarkSystem = 12;
                    categoryTable.MarkSystem = 12;
                    break;
                case 5:
                    table.MarkSystem = 25;
                    categoryTable.MarkSystem = 25;
                    break;
            }
        }
    }
}
