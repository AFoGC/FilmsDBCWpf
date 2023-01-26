using System.IO;
using TablesLibrary.Interpreter;

namespace WpfApp.Models
{
    public class ProfileModel
    {
        private readonly string _profilesPath;
        public readonly string ProfleDirectoryPath;
        public readonly string ProfileFilePath;

        public string Name { get; }

        public ProfileModel(string name, string profilesPath)
        {
            Name = name;
            _profilesPath = profilesPath;
            ProfleDirectoryPath = Path.Combine(_profilesPath, Name);
            ProfileFilePath = Path.Combine(ProfleDirectoryPath, "Films.fdbc");
        }

        public void CreateProfileDirectory(TableCollection defaultCollection)
        {
            
        }
    }
}
