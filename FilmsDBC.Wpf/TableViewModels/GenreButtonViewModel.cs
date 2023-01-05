using FilmsDBC.Wpf.TableViewModels.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TL_Objects.Interfaces;

namespace WpfApp.TableViewModels
{
    public class GenreButtonViewModel : INotifyPropertyChanged, IGenreButton
    {
        private bool _isChecked = true;

        public GenreButtonViewModel(IGenre genre)
        {
            Model = genre;
            Model.PropertyChanged += ModelPropertyChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public IGenre Model { get; private set; }
        public String Name => Model.Name;

        public bool IsChecked
        {
            get => _isChecked;
            set
            {
                _isChecked = value;
                OnPropertyChanged();
            }
        }

        private void ModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        private void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }
    }
}
