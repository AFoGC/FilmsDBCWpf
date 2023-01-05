using NewTablesLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace TL_Objects.CellDataClasses
{
    public class Mark : INotifyPropertyChanged, ILoadField
    {
        public const int MaxRawMark = 300;
        private int _maxMark;
        private int _rawMark;

        public Mark()
        {
            MarkSystem = 6;
            RawMark = 0;
        }

        public int MarkSystem
        {
            get => _maxMark;
            set { _maxMark = value; OnPropertyChanged(nameof(MarkSystem)); }
        }
        public int RawMark
        {
            get => _rawMark;
            set { _rawMark = value; OnPropertyChanged(nameof(RawMark)); }
        }
        public int FormatedMark
        {
            get
            {
                int modifier = MaxRawMark / MarkSystem;
                int outMark = 0;
                for (int i = 0; i <= MarkSystem; i++)
                {
                    outMark = modifier * i;
                    if (outMark >= RawMark)
                    {
                        return i;
                    }
                }
                
                return 0;
            }
            set
            {
                int modifier = MaxRawMark / MarkSystem;
                RawMark = modifier * value;
            }
        }

        public override string ToString()
        {
            return RawMark.ToString();
        }

        public void FromString(string value)
        {
            int i = Convert.ToInt32(value);

            if (i > 300) i = 300;
            if (i < 0) i = 0;

            RawMark = i;
        }

        public List<String> GetComboItems()
        {
            List<String> strs = new List<String>();

            for (int i = 1; i <= _maxMark; i++)
            {
                strs.Add($"{i}/{MarkSystem}");
            }

            strs.Add(String.Empty);

            return strs;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
