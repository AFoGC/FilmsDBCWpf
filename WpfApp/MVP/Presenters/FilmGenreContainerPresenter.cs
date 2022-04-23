using FilmsUCWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using WpfApp.MVP.ViewsInterface;

namespace WpfApp.MVP.Presenters
{
    public class FilmGenreContainerPresenter : IGenreContainerPresenter
    {
        private readonly IGenreSettingsContainerView view;
        private readonly TableCollection tableCollection;
        public FilmGenreContainerPresenter(IGenreSettingsContainerView view, TableCollection tableCollection)
        {
            this.view = view;
            this.tableCollection = tableCollection;
            tableCollection.TableLoad += TableCollection_TableLoad;
        }

        private void TableCollection_TableLoad(object sender, EventArgs e)
        {
            RefreshControl();
        }

        public void AddGenre()
        {
            Genre genre = new Genre();
            genre.Name = $"Genre{genre.ID}";
            tableCollection.GetTable<Genre>().AddElement(genre);
            view.GenreControls.Add(new FilmGenreControl(genre, tableCollection));
            view.Height += 20;
        }

        public void RefreshControl()
        {
            view.Height = 60;
            view.GenreControls.Clear();
            foreach (Genre genre in tableCollection.GetTable<Genre>())
            {
                view.GenreControls.Add(new FilmGenreControl(genre, tableCollection));
                view.Height += 20;
            }
        }
    }
}
