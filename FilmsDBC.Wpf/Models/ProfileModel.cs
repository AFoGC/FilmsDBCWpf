using System.IO;

namespace WpfApp.Models
{
    public class ProfileModel
    {
        private readonly string _profilesPath;

        public string Name { get; private set; }

        public ProfileModel(string name, string profilesPath)
        {
            Name = name;
            _profilesPath = profilesPath;
        }

        public void CreateProfileDirectory()
        {
            string directoryPath = GetProfileDirectoryPath();
            string mainFilePath = GetProfileMainFilePath();

            Directory.CreateDirectory(directoryPath);
            if (File.Exists(mainFilePath) == false)
            {
                File.Create(mainFilePath);
            }
        }

        public string GetProfileMainFilePath()
        {
            return Path.Combine(GetProfileDirectoryPath(), "Films.fdbc");
        }

        public string GetProfileDirectoryPath()
        {
            return Path.Combine(_profilesPath, Name);
        }
    }
}
