using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.Services
{
	[Serializable]
	public class StartUserInfo
	{
		public StartUserInfo()
		{
			LoggedIn = false;
			Email = String.Empty;
			Password = String.Empty;
		}
		public Boolean LoggedIn { get; set; }
		public String Email { get; set; }
		public String Password { get; set; }
	}
}
