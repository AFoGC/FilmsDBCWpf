using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
	public class SettingsFields
	{
		public String UsedProfile { get; set; }
		public int MarkSystem { get; set; }
		public StartUserInfo StartUser { get; set; }

		public SettingsFields()
		{
			UsedProfile = String.Empty;
			MarkSystem = 0;
			StartUser = new StartUserInfo();
		}
	}
}
