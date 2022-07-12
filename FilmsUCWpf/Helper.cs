using FilmsUCWpf.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace FilmsUCWpf
{
    public static class Helper
    {
        public static String MarkToText(String mark)
        {
			switch (mark)
			{
				case "":
					return mark;

				case "6":
					return mark + "/6";

				default:
					return mark + "/5";
			}
		}

		public static String SourcesStateString(TLCollection<Source> sources)
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

        public static void CopyFirstSource(TLCollection<Source> sources)
        {
            if (sources.Count != 0)
            {
                Clipboard.SetText(sources[0].SourceUrl);
            }
        }

		public static int TextToInt32(String str)
		{
			if (str == "")
			{
				return 0;
			}
			else
			{
				return Convert.ToInt32(str);
			}
		}

		public static sbyte TextToMark(String str)
		{
			if (str == "")
			{
				return -1;
			}
			else
			{
				return Convert.ToSByte(str.Substring(0, str.IndexOf('/')));
			}
		}

		internal static void OpenSources(IBaseMenu menu, TLCollection<Source> sources)
        {
			menu.UpdateFormVisualizer.SourcesVisualizer.OpenSourceControl(sources);
        }

		public static List<String> GetAllMarks()
		{
			List<String> export = new List<String>();

			export.Add("6/6");
			export.Add("5/5");
			export.Add("4/5");
			export.Add("3/5");
			export.Add("2/5");
			export.Add("1/5");
			export.Add("0/5");

			return export;
		}
	}
}
