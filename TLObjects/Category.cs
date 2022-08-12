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
        private string name = "";
        private string hideName = String.Empty;
        private Mark mark = new Mark();
        private int priority = 0;

        private ObservableCollection<Film> films = new ObservableCollection<Film>();

        public Category()
        {
            films.CollectionChanged += Films_CollectionChanged;
            mark.PropertyChanged += Mark_PropertyChanged;
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
            if (films.Contains(film))
            {
                if (film.FranshiseId == this.ID)
                {
                    film.FranshiseId = 0;
                    film.FranshiseListIndex = -1;
                }
                return films.Remove(film);
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

            name = category.name;
            hideName = category.hideName;
            mark = category.mark;
            priority = category.priority;
            films = category.films;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", name, "", 2));
            streamWriter.Write(FormatParam("hideName", hideName, String.Empty, 2));
            streamWriter.Write(FormatParam("mark", mark.RawMark, 0, 2));
            streamWriter.Write(FormatParam("priority", priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    name = comand.Value;
                    break;
                case "hideName":
                    hideName = comand.Value;
                    break;
                case "mark":
                    mark.RawMark = Convert.ToInt32(comand.Value);
                    break;
                case "priority":
                    priority = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(nameof(Name)); }
        }

        public string HideName
        {
            get { return hideName; }
            set 
            { 
                hideName = value;
                foreach (Film film in Films)
                {
                    film.OnPropertyChanged(nameof(film.Name));
                }
                OnPropertyChanged(nameof(HideName)); 
            }
        }

        public int Mark
        {
            get { return mark.RawMark; }
            set { mark.RawMark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public Mark FormatedMark
        {
            get => mark;
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public ObservableCollection<Film> Films
        {
            get { return films; }
        }
    }
}
