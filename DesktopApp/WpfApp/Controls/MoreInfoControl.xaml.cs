using System;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Views.Interfaces;

namespace WpfApp.Windows
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
