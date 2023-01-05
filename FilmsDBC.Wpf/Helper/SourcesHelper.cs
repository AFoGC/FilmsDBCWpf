using FilmsDBC.Wpf.Helper;
using System;
using System.Collections.ObjectModel;
using TL_Objects.CellDataClasses;

namespace WpfApp.Helper
{
    public static class SourcesHelper
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
                ClipboardHelper.SetText(sources[0].SourceUrl);
            }
        }
    }
}
