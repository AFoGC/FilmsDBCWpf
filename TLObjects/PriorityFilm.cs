using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Attributes;

namespace TL_Objects
{
    [TableCell("PriorityFilm")]
    public class PriorityFilm : Cell
    {
        public Film Film
        {
            get { return film; }
            set
            {
                film = value;
                FilmId = film.ID;
            }
        }
        private Film film;
        public int FilmId { get; private set; } = 0;


        public PriorityFilm() : base() { }
        public PriorityFilm(int id) : base(id) { }


        protected override void updateThisBody(Cell cell)
        {
            PriorityFilm priorityFilm = (PriorityFilm)cell;

            Film = priorityFilm.Film;
        }

        protected override void saveBody(StreamWriter streamWriter)
        {
            streamWriter.Write(FormatParam("film", FilmId, 0, 2));
        }

        protected override void loadBody(Comand comand)
        {
            switch (comand.Paramert)
            {
                case "film":
                    FilmId = Convert.ToInt32(comand.Value);
                    break;

                default:
                    break;
            }
        }
    }
}
