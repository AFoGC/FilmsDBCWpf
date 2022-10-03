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
using WpfApp.Models;
using WpfApp.Presenters;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для AutosaveSettingView.xaml
    /// </summary>
    public partial class AutosaveSettingView : UserControl
    {
        public AutosaveSettingView(SettingsModel settings)
        {
            InitializeComponent();
            DataContext = new AutosaveSettingsPresenter(settings);
        }
    }
}
