using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    }
}
