namespace WpfApp.Models
{
    public class ProfileModel
    {
        public string Name { get; private set; }

        public ProfileModel(string name)
        {
            Name = name;
        }
    }
}
