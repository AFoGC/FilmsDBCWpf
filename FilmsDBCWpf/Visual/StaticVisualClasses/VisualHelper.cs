using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsDBCWpf.Visual.StaticVisualClasses
{
    class VisualHelper
    {
		public static String markToText(String mark)
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
	}
}
