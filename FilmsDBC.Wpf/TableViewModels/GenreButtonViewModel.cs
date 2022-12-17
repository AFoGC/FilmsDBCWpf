using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TL_Objects.Interfaces;

namespace WpfApp.TableViewModels
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
