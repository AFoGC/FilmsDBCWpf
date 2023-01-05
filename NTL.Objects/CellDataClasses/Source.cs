using System.ComponentModel;

namespace TL_Objects.CellDataClasses
{
    public class Source : INotifyPropertyChanged
    {
        private string _name;
        private string _sourceUrl;

        public Source()
        {
            Name = String.Empty;
            SourceUrl = String.Empty;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String Name 
        {
            get => _name;
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }
        public String SourceUrl
        {
            get => _sourceUrl;
            set { _sourceUrl = value; OnPropertyChanged(nameof(SourceUrl)); }
        }

        public static Source ToSource(String import)
        {
            int komaIndex = import.IndexOf(',');
            if (komaIndex == -1)
            {
                return new Source
                {
                    SourceUrl = import
                };
            }
            else
            {
                return new Source
                {
                    Name = import.Substring(0, komaIndex),
                    SourceUrl = import.Substring(komaIndex + 2)
                };
            }
        }

        public override string ToString()
        {
            if (Name == String.Empty)
            {
                return SourceUrl;
            }
            else
            {
                return Name + ", " + SourceUrl;
            }
        }

        public void OnPropertyChanged(String propertyName)
        {
            var e = new PropertyChangedEventArgs(propertyName);
            PropertyChanged?.Invoke(this, e);
        }
    }
}
