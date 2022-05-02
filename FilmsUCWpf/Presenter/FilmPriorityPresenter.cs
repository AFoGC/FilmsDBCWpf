using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
    public class FilmPriorityPresenter : FilmPresenter
    {
        public PriorityFilm PriorityModel { get; protected set; }
        public FilmPriorityPresenter(PriorityFilm priorityFilm, IView view, IMenu<Film> menu, TableCollection tableCollection) : base(priorityFilm.Film, view, menu, tableCollection)
        {
            this.PriorityModel = priorityFilm;
            PriorityModel.CellRemoved += PriorityModel_CellRemoved;
        }

        private void PriorityModel_CellRemoved(object sender, EventArgs e)
        {
            View.SelfRemove();
        }

        public void RemoveFromPriority()
        {
            PriorityFilmsTable priorityFilms = (PriorityFilmsTable)TableCollection.GetTable<PriorityFilm>();
            priorityFilms.Remove(PriorityModel);
        }
    }
}
