using FilmsUCWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
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
            RefreshControl();
        }

        private void TableCollection_TableLoad(object sender, EventArgs e)
        {
            RefreshControl();
        }

        public void AddGenre()
        {
            Genre genre = new Genre();
            tableCollection.GetTable<Genre>().AddElement(genre);
            genre.Name = $"Genre{genre.ID}";
            view.GenreControls.Add(new FilmGenreControl(genre, tableCollection));
            view.Height += 20;
        }

        public void RefreshControl()
        {
            view.Height = view.DefaultHeight;
            view.GenreControls.Clear();
            foreach (Genre genre in tableCollection.GetTable<Genre>())
            {
                view.GenreControls.Add(new FilmGenreControl(genre, tableCollection));
                view.Height += 20;
            }
        }
    }
}
