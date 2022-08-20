using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TL_Objects.CellDataClasses
{
    public class Source : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _name;
        private string _sourceUrl;
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

        public Source()
        {
            Name = String.Empty;
            SourceUrl = String.Empty;
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
            if (Name == "")
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
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }
    }
}
