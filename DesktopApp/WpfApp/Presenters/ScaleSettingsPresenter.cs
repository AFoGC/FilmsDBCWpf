using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp.Models;

namespace WpfApp.Presenters
{
    public class ScaleSettingsPresenter
    {
        public enum ScaleEnum
        {
            Small = 0,
            Medium = 1
        }

        private ScaleEnum scale;
        private readonly ProgramSettings settings;

        public ScaleSettingsPresenter(ProgramSettings settings)
        {
            this.settings = settings;
            if (Enum.IsDefined(typeof(ScaleEnum), settings.Scale))
            {
                Scale = settings.Scale;
            }
            else
            {
                Scale = (int)ScaleEnum.Small;
            }
        }

        
        public int Scale
        {
            get => (int)scale;
            set
            {
                if (Enum.IsDefined(typeof(ScaleEnum), value))
                {
                    scale = (ScaleEnum)value;
                    settings.Scale = value;
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
        }
    }
}
