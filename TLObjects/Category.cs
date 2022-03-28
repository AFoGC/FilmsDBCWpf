using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace TL_Objects
{
    [TableCell("Category")]
    public class Category : Cell
    {
        private string name = "";
        private sbyte mark = -1;
        private int priority = 0;

        private TLCollection<Film> films = new TLCollection<Film>();

        public Category() : base() { }
        public Category(int id) : base(id) { }

        public bool RemoveFilmFromCategory(Film film)
        {
            if (films.Contains(film))
            {
                foreach (Film item in films)
                {
                    if (film.FranshiseListIndex < item.FranshiseListIndex)
                    {
                        --item.FranshiseListIndex;
                    }
                }
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

        protected override void updateThisBody(Cell cell)
        {
            Category category = (Category)cell;

            name = category.name;
            mark = category.mark;
            priority = category.priority;
            films = category.films;
        }

        protected override void saveBody(StreamWriter streamWriter, Cell defaultCell)
        {
            streamWriter.Write(FormatParam("name", name, "", 2));
            streamWriter.Write(FormatParam("mark", mark, -1, 2));
            streamWriter.Write(FormatParam("priority", priority, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "name":
                    name = comand.Value;
                    break;
                case "mark":
                    mark = Convert.ToSByte(comand.Value);
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

        public sbyte Mark
        {
            get { return mark; }
            set { mark = value; OnPropertyChanged(nameof(Mark)); }
        }

        public int Priority
        {
            get { return priority; }
            set { priority = value; OnPropertyChanged(nameof(Priority)); }
        }

        public TLCollection<Film> Films
        {
            get { return films; }
            set { films = value; OnPropertyChanged(nameof(Films)); }
        }
    }
}
