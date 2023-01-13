using System.IO;
using System.Reflection;

namespace WpfApp.Helper
{
    public static class PathHelper
    {
        public static readonly string MainDirectory;
        public static readonly string ProfilesPath;
        public static readonly string SettingsPath;
        

        static PathHelper()
        {
            MainDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            SettingsPath = "ProgramSetting.xml";
            ProfilesPath = "Profiles";
        }
    }
}
