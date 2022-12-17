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
            SettingsPath = Path.Combine(MainDirectory, "ProgramSetting.xml");
            ProfilesPath = Path.Combine(MainDirectory, "Profiles");
        }

        public static string GetProfileFilePath(string profileName)
        {
            return Path.Combine(ProfilesPath, profileName, "Films.fdbc");
        }
    }
}
