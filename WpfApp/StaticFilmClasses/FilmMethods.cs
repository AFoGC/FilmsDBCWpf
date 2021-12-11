using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.StaticFilmClasses
{
    public static class FilmMethods
    {
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
