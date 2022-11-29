using System;
using System.IO;
using System.Text;
using System.Windows.Threading;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;
using WpfApp.Helper;

namespace WpfApp.Services
{
    public class TablesFileService
    {
        public event Action AutosaveIsEnableChanged;
        public event Action AutosaveIntervalChanged;
        public event Action MarkSystemCanged;

        private readonly TableCollection _tablesCollection;

        private DispatcherTimer _saveTimer;
        private bool _isAutosaveEnable = false;
        private double _autosaveInterval = 30;

        public TablesFileService()
        {
            _saveTimer = new DispatcherTimer();

            _tablesCollection = GetDefaultTableCollection();

            _tablesCollection.CellInTablesChanged += (s, e) => StartSaveTimer();
            _tablesCollection.TableSave += (s, e) => StopSaveTimer();
            _saveTimer.Tick += (s, e) => Autosave();
        }

        public TableCollection TablesCollection => _tablesCollection;
        public Double SaveTimerInterval
        {
            get => _autosaveInterval;
            set
            {
                _autosaveInterval = value;
                _saveTimer.Interval = TimeSpan.FromSeconds(value);
                AutosaveIntervalChanged?.Invoke();
            }

        }
        public bool IsAutosaveEnable
        {
            get => _isAutosaveEnable;
            set
            {
                _isAutosaveEnable = value;
                StopSaveTimer();
                AutosaveIsEnableChanged?.Invoke();
            }
        }

        public int FilmMarkSystem
        {
            get => FilmsTable.MarkSystem;
            set
            {
                FilmsTable.MarkSystem = value;
                FilmCategoriesTable.MarkSystem = value;
                MarkSystemCanged?.Invoke();
            }
        }

        public int BookMarkSystem
        {
            get => BooksTable.MarkSystem;
            set
            {
                BooksTable.MarkSystem = value;
                BookCategoriesTable.MarkSystem = value;
                MarkSystemCanged?.Invoke();
            }
        }

        public CategoriesTable FilmCategoriesTable => (CategoriesTable)_tablesCollection.GetTable<Category>();
        public GenresTable FilmGenresTable => (GenresTable)_tablesCollection.GetTable<Genre>();
        public FilmsTable FilmsTable => (FilmsTable)_tablesCollection.GetTable<Film>();
        public SeriesTable SeriesTable => (SeriesTable)_tablesCollection.GetTable<Serie>();
        public PriorityFilmsTable PriorityFilmsTable => (PriorityFilmsTable)_tablesCollection.GetTable<PriorityFilm>();
        public BookGenresTable BookGenresTable => (BookGenresTable)_tablesCollection.GetTable<BookGenre>();
        public BooksTable BooksTable => (BooksTable)_tablesCollection.GetTable<Book>();
        public BookCategoriesTable BookCategoriesTable => (BookCategoriesTable)_tablesCollection.GetTable<BookCategory>();
        public PriorityBooksTable PriorityBooksTable => (PriorityBooksTable)_tablesCollection.GetTable<PriorityBook>();

        private static TableCollection GetDefaultTableCollection()
        {
            TableCollection export = new TableCollection();
            export.FileEncoding = Encoding.UTF8;

            export.AddTable(new CategoriesTable());
            export.AddTable(GenresTable.GetDefaultGenresTable());
            export.AddTable(new FilmsTable());
            export.AddTable(new SeriesTable());
            export.AddTable(new PriorityFilmsTable());
            export.AddTable(BookGenresTable.GetDefaultGenresTable());
            export.AddTable(new BooksTable());
            export.AddTable(new BookCategoriesTable());
            export.AddTable(new PriorityBooksTable());

            return export;
        }

        /// <summary>
        /// Creates profiles direcotry and file if didn't exist
        /// </summary>
        /// <returns>Full path of profile directory</returns>
        private string CreateProfileFile(string profileName)
        {
            string path = PathHelper.GetProfileFilePath(profileName);
            if (File.Exists(path) == false)
                File.Create(path).Dispose();

            return path;
        }

        public bool LoadTables(string profileName)
        {
            _tablesCollection.TableFilePath = CreateProfileFile(profileName);
            return _tablesCollection.LoadTables();
        }

        public void SaveTables()
        {
            _tablesCollection.SaveTables();
        }

        private void StartSaveTimer()
        {
            if (_saveTimer.IsEnabled)
                _saveTimer.Stop();

            if (IsAutosaveEnable)
                _saveTimer.Start();
        }

        private void StopSaveTimer()
        {
            _saveTimer.Stop();
        }

        private void Autosave()
        {
            _saveTimer.Stop();
            _tablesCollection.SaveTables();
        }
    }
}
