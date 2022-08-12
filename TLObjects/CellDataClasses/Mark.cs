using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace TL_Objects.CellDataClasses
{
    public class Mark : INotifyPropertyChanged
    {
        public const int MaxRawMark = 300;
        private int maxMark;
        private int rawMark;

        public Mark()
        {
            MarkSystem = 6;
            RawMark = 0;
        }

        public int MarkSystem
        {
            get => maxMark;
            set { maxMark = value; OnPropertyChanged(nameof(MarkSystem)); }
        }
        public int RawMark
        {
            get => rawMark;
            set { rawMark = value; OnPropertyChanged(nameof(RawMark)); }
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
            int outMark = FormatedMark;
            if (outMark != 0)
            {
                return $"{outMark}/{MarkSystem}";
            }
            else
            {
                return String.Empty;
            }
        }

        public void SetMarkFromString(String str)
        {
            string cleanedString = str.Substring(0, str.IndexOf('/'));
            int mark = Convert.ToInt32(cleanedString);
            FormatedMark = mark;
        }

        public List<String> GetComboItems()
        {
            List<String> strs = new List<String>();

            for (int i = 1; i <= maxMark; i++)
            {
                strs.Add($"{i}/{MarkSystem}");
            }

            return strs;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }


    }
}
