using FilmsUCWpf.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;
using WpfApp.Views.Interfaces;

namespace WpfApp.Presenters
{
    public class BookGenreContainerPresenter : IGenreContainerPresenter
    {
        private readonly IGenreSettingsContainerView view;
        private readonly TableCollection tableCollection;
        public BookGenreContainerPresenter(IGenreSettingsContainerView view, TableCollection tableCollection)
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
            BookGenre genre = new BookGenre();
            tableCollection.GetTable<BookGenre>().AddElement(genre);
            genre.Name = $"Genre{genre.ID}";
            view.GenreControls.Add(new BookGenreControl(genre, tableCollection));
            view.Height += 20;
        }

        public void RefreshControl()
        {
            view.Height = view.DefaultHeight;
            view.GenreControls.Clear();
            foreach (BookGenre genre in tableCollection.GetTable<BookGenre>())
            {
                view.GenreControls.Add(new BookGenreControl(genre, tableCollection));
                view.Height += 20;
            }
        }
    }
}
