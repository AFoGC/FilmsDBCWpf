using System;
using TL_Objects;
using TL_Tables;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class FilmsModel
    {
        private readonly TablesService _tablesService;

        public event Action TablesLoaded;

        public FilmsModel(TablesService tablesService)
        {
            _tablesService = tablesService;
            _tablesService.TablesCollection.TablesSaved += OnTableLoad;
        }

        public FilmsTable FilmsTable => _tablesService.FilmsTable;
        public SeriesTable SeriesTable => _tablesService.SeriesTable;
        public CategoriesTable CategoriesTable => _tablesService.FilmCategoriesTable;
        public GenresTable GenresTable => _tablesService.FilmGenresTable;
        public PriorityFilmsTable PriorityFilmsTable => _tablesService.PriorityFilmsTable;

        private void OnTableLoad()
        {
            TablesLoaded?.Invoke();
        }

        public void AddCategory()
        {
            CategoriesTable.Add(new Category());
        }

        public void AddFilm()
        {
            Film film = new Film();
            film.Genre = GenresTable[0];
            FilmsTable.Add(film);
        }

        public void SaveTables() { }//_tablesService.TablesCollection.SaveTables();
    }
}
