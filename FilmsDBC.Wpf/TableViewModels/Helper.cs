using System;
using System.Collections.ObjectModel;
using System.Windows;
using TL_Objects.CellDataClasses;

namespace WpfApp.TableViewModels
{
    public static class Helper
    {
		public static String SourcesStateString(ObservableCollection<Source> sources)
        {
            if (sources.Count == 0)
            {
                return "no url";
            }
            else
            {
                if (sources[0].Name != "")
                {
                    return "url: " + sources[0].Name;
                }
                else
                {
                    return "copy url";
                }
            }
        }

        public static void CopyFirstSource(ObservableCollection<Source> sources)
        {
            if (sources.Count != 0)
            {
                Clipboard.SetText(sources[0].SourceUrl);
            }
        }
	}
}
