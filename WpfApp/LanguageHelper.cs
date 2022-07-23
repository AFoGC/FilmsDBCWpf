using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml.Serialization;

namespace WpfApp
{
    public static class LanguageHelper
    {
        static LanguageHelper()
        {
            Cultures = new List<CultureInfo>();
            Cultures.Add(new CultureInfo("ru"));
            Cultures.Add(new CultureInfo("uk-UA"));

            LoadLang();
        }

        public static List<CultureInfo> Cultures { get; private set; }

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                if (Cultures.Contains(value))
                {
                    dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
                }
                else
                {
                    dict.Source = new Uri("Resources/Localizations/lang.xaml", UriKind.Relative);
                }

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
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

                //4. Вызываем евент для оповещения всех окон.
            }
        }

        private static string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "lang");
        public static void SaveLang()
        {
            using (StreamWriter fs = new StreamWriter(path, false, Encoding.UTF8))
            {
                fs.WriteLine(Language.ToString());
            }
        }

        public static void LoadLang()
        {
            if (File.Exists(path))
            {
                using (StreamReader fs = new StreamReader(path, Encoding.UTF8))
                {
                    Language = new CultureInfo(fs.ReadLine());
                }
            }
            else
            {
                Language = Cultures[0];
            }
        }
    }
}
