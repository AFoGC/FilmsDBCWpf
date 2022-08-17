using FilmsUCWpf.View.Interfaces;
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
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для MoreInfoControl.xaml
    /// </summary>
    public partial class MoreInfoControl : UserControl
    {
        private readonly IBaseMenuView menu;
        public MoreInfoControl(IBaseMenuView menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        public void Open(Object uiElmenet)
        {
            ElementFrame.Content = uiElmenet;
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            menu.CloseAllInfos();
        }
    }
}
