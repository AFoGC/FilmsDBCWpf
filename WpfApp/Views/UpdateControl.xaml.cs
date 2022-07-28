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
    /// Логика взаимодействия для UpdateControl.xaml
    /// </summary>
    public partial class UpdateControl : UserControl
    {
        private readonly IBaseMenuView menu;
        public UpdateControl(IBaseMenuView menu)
        {
            InitializeComponent();
            this.menu = menu;
        }

        public void Open(IUpdateControl updateControl)
        {
            ElementFrame.Content = updateControl;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Update();
        }

        public void Update()
        {
            if (ElementFrame.Content != null)
            {
                IUpdateControl updateControl = (IUpdateControl)ElementFrame.Content;
                updateControl.UpdateElement();
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            menu.CloseAllInfos();
        }
    }
}
