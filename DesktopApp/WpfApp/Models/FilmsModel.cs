using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
    public class FilmsModel
    {
        public TableCollection TableCollection { get; private set; }
        public FilmsTable FilmsTable { get; private set; }
        public CategoriesTable CategoriesTable { get; private set; }
        public GenresTable GenresTable { get; private set; }
        public PriorityFilmsTable PriorityFilmsTable { get; private set; }

        public FilmsModel()
        {
            TableCollection collection = SettingsModel.Initialize().TableCollection;
            TableCollection = collection;

            FilmsTable = (FilmsTable)collection.GetTable<Film>();
            CategoriesTable = (CategoriesTable)collection.GetTable<Category>();
            GenresTable = (GenresTable)collection.GetTable<Genre>();
            PriorityFilmsTable = (PriorityFilmsTable)collection.GetTable<PriorityFilm>();
        }

        public void AddCategory()
        {
            CategoriesTable.AddElement(new Category());
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.Genre = GenresTable[0];
            FilmsTable.AddElement(film);
        }

        public void SaveTables() => TableCollection.SaveTables();
    }
}
