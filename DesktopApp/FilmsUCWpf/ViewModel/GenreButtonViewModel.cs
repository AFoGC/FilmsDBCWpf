using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.ViewModel
{
    public class GenreButtonViewModel: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public IGenre Model { get; private set; }

        private bool _isChecked = true;
        public bool IsChecked
        { 
            get => _isChecked; 
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }
        public String Name => Model.Name;
        public GenreButtonViewModel(IGenre genre)
        {
            Model = genre;
            Model.PropertyChanged += ModelPropertyChanged;
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
