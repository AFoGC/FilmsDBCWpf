using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace TL_Objects
{
    [TableCell("Category")]
    public class Category : Cell
    {
        private string _name;
        private string _hideName;
        private Mark _mark;
        private int _priority;

        private ObservableCollection<Film> _films;

        public Category()
        {
            _name = String.Empty;
            _hideName = String.Empty;
            _mark = new Mark();
            _priority = 0;
            _films = new ObservableCollection<Film>();

            _films.CollectionChanged += Films_CollectionChanged;
            _mark.PropertyChanged += Mark_PropertyChanged;
        }

        private void Mark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            OnPropertyChanged(nameof(FormatedMark));
        }

        private void Films_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Film film;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Remove:
                    film = (Film)e.OldItems[0];
                    film.FranshiseId = 0;
                    film.FranshiseListIndex = -1;
                    break;
                case NotifyCollectionChangedAction.Add:
                    film = (Film)e.NewItems[0];
                    film.FranshiseId = this.ID;
                    break;

                default:
                    break;
            }

            sbyte i = 0;
            foreach (Film item in Films)
            {
                item.FranshiseListIndex = i++;
            }
        }

        public bool RemoveFilmFromCategory(Film film)
        {
            if (_films.Contains(film))
            {
                if (film.FranshiseId == this.ID)
                {
                    film.FranshiseId = 0;
                    film.FranshiseListIndex = -1;
                }
                return _films.Remove(film);
            }
            else
            {
                return false;
            }
        }

        
        public bool ChangeFilmPositionBy(Film film, int i)
        {
            int oldIndex = Films.IndexOf(film);
            int newIndex = oldIndex + i;

            if (newIndex > -1 && newIndex < Films.Count)
            {
                Films.Move(oldIndex, newIndex);
                return true;
            }
            return false;
        }
        

        protected override void updateThisBody(Cell cell)
        {
            Category category = (Category)cell;

            _name = category._name;
            _hideName = category._hideName;
            _mark = category._mark;
            _priority = category._priority;
            _films = category._films;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", _name, String.Empty, 2));
            streamWriter.Write(FormatParam("hideName", _hideName, String.Empty, 2));
            streamWriter.Write(FormatParam("mark", _mark.RawMark, 0, 2));
            streamWriter.Write(FormatParam("priority", _priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    _name = comand.Value;
                    break;
                case "hideName":
                    _hideName = comand.Value;
                    break;
                case "mark":
                    _mark.RawMark = Convert.ToInt32(comand.Value);
                    break;
                case "priority":
                    _priority = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string HideName
        {
            get { return _hideName; }
            set 
            { 
                _hideName = value;
                foreach (Film film in Films)
                {
                    film.OnPropertyChanged(nameof(film.Name));
                }
                OnPropertyChanged(nameof(HideName)); 
            }
        }

        public int Mark
        {
            get { return _mark.RawMark; }
            set { _mark.RawMark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public Mark FormatedMark
        {
            get => _mark;
        }

        public int Priority
        {
            get { return _priority; }
            set { _priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public ObservableCollection<Film> Films
        {
            get { return _films; }
        }
    }
}
