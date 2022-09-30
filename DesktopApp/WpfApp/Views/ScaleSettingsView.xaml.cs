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
    /// Логика взаимодействия для ScaleSettingsView.xaml
    /// </summary>
    public partial class ScaleSettingsView : UserControl
    {
        public ScaleSettingsView(ProgramSettings settings)
        {
            InitializeComponent();
            DataContext = new ScaleSettingsPresenter(settings);
        }
    }
}
