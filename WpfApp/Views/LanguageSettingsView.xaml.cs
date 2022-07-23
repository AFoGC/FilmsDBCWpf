using System;
using System.Collections.Generic;
using System.Globalization;
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

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для LanguageSettingsView.xaml
    /// </summary>
    public partial class LanguageSettingsView : UserControl
    {
        public LanguageSettingsView()
        {
            InitializeComponent();
            langPanel.Items.Add(new CultureInfo("en").NativeName);
            foreach (CultureInfo culture in LanguageHelper.Cultures)
            {
                langPanel.Items.Add(culture.NativeName);
            }
            langPanel.SelectedIndex = 0;
            langPanel.SelectedItem = LanguageHelper.Language.NativeName;
        }

        private void ChangeLang(object sender, RoutedEventArgs e)
        {
            foreach (CultureInfo culture in LanguageHelper.Cultures)
            {
                if (langPanel.Text == culture.NativeName)
                {
                    LanguageHelper.Language = culture;
                    return;
                }
            }
            LanguageHelper.Language = new CultureInfo("en");
        }
    }
}
