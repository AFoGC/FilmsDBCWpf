using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using WpfApp.Models;

namespace WpfApp.Presenters
{
    public class LanguageSettingsPresenter
    {
        public List<CultureInfo> Cultures { get; private set; }
        private readonly ProgramSettings settings;

        public LanguageSettingsPresenter(ProgramSettings settings)
        {
            this.settings = settings;
            Cultures = new List<CultureInfo>();
            Cultures.Add(new CultureInfo("ru"));
            Cultures.Add(new CultureInfo("uk-UA"));

            Language = new CultureInfo(settings.Lang);
        }

        public CultureInfo Language
        {
            get
            {
                return Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == Thread.CurrentThread.CurrentUICulture) return;

                //1. Change Application Language:
                Thread.CurrentThread.CurrentUICulture = value;

                //2. Creating ResourceDictionary for new culture
                ResourceDictionary dict = new ResourceDictionary();
                if (Cultures.Contains(value))
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

                settings.SaveSettings();
            }
        }
    }
}
