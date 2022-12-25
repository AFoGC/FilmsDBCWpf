using FilmsDBC.Wpf.TableViewModels.Interfaces;
using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.Interfaces;

namespace WpfApp.TableViewModels.Interfaces
{
    public abstract class BaseViewModel<T> : IBaseViewModel, INotifyPropertyChanged where T : Cell
    {
        protected readonly DateTime defaultDate = new DateTime();
        private bool _isSelected = false;
        private bool _isFinded = true;

        public BaseViewModel(T model)
        {
            Model = model;
            TableCollection = Model.ParentTable.TableCollection;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public T Model { get; }
        protected TableCollection TableCollection { get; }
        
        public bool IsSelected
        {
            get => _isSelected;
            set { _isSelected = value; OnPropertyChanged(); }
        }
        
        public bool IsFinded
        {
            get => _isFinded;
            set { _isFinded = value; OnPropertyChanged(); }
        }

        public abstract bool HasSelectedGenre(IGenre[] selectedGenres);
        public abstract bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked);
        public abstract bool SetFinded(string search);

        protected string FormateDate(DateTime date)
        {
            if (date != defaultDate)
                return date.ToShortDateString();
            else
                return String.Empty;
        }

        protected string formatZero(int import)
        {
            return Cell.FormatToString(import, 0);
        }

        protected int formatEmpty(string import)
        {
            int export = 0;
            if (import != String.Empty)
            {
                Int32.TryParse(import, out export);
            }

            return export;
        }

        protected void OnPropertyChanged(PropertyChangedEventArgs e)
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
