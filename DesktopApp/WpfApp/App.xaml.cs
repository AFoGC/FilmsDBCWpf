using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using System.Windows.Documents;
using WpfApp.Models;
using WpfApp.Properties;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            //SettingsModel settings = SettingsModel.Initialize();
            //settings.LanguageChanged += LanguageChanged;
            //settings.ScaleChanged += ScaleChanged;
        }

        private void ScaleChanged(ScaleEnum value)
        {
            if (Enum.IsDefined(typeof(ScaleEnum), value))
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(String.Format("Resources/Dictionaries/TableControls/Scale.{0}.xaml", value), UriKind.Relative);

                ResourceDictionary oldDict =
                    (from d in Application.Current.Resources.MergedDictionaries
                     where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Dictionaries/TableControls/Scale.")
                     select d).First();

                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }
            }
        }

        private void LanguageChanged(CultureInfo value, List<CultureInfo> cultures)
        {
            if (value == null) throw new ArgumentNullException("value");
            if (value == Thread.CurrentThread.CurrentUICulture) return;

            //1. Change Application Language:
            Thread.CurrentThread.CurrentUICulture = value;

            //2. Creating ResourceDictionary for new culture
            ResourceDictionary dict = new ResourceDictionary();
            if (cultures.Contains(value) && value.Name != "en")
            {
                dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
            }
            else
            {
                dict.Source = new Uri("Resources/Localizations/lang.xaml", UriKind.Relative);
            }

            //3. Find old ResourceDictionary delete it and add new ResourceDictionary
            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Localizations/lang.")
                                          select d).First();
            if (oldDict != null)
            {
                int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(dict);
            }
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                e.Exception.ToString(), 
                "Critical Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error
                );
        }
    }
}
