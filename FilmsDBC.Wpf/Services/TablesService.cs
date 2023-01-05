using System;
using System.IO;
using System.Text;
using TL_Objects;
using TL_Tables;
using WpfApp.Helper;
using NewTablesLibrary;

namespace WpfApp.Services
{
    public class TablesService
    {
        public event Action AutosaveIsEnableChanged;
        public event Action AutosaveIntervalChanged;
        public event Action MarkSystemCanged;

        private readonly TablesCollection _tablesCollection;

        private System.Timers.Timer _saveTimer;
        private bool _isAutosaveEnable = false;
        private double _autosaveInterval = 30;

        public TablesService()
        {
            _saveTimer = new System.Timers.Timer();

            _tablesCollection = GetDefaultTableCollection();

            _tablesCollection.DataChanged += StartSaveTimer;
            _tablesCollection.TablesSaved += StopSaveTimer;
            _saveTimer.Elapsed += (s, e) => Autosave();
        }

        public TablesCollection TablesCollection => _tablesCollection;
        public Double SaveTimerInterval
        {
            get => _autosaveInterval;
            set
            {
                _autosaveInterval = value;
                _saveTimer.Interval = value * 1000;
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

        public CategoriesTable FilmCategoriesTable => _tablesCollection.GetTableByTableType<CategoriesTable>();
        public GenresTable FilmGenresTable => _tablesCollection.GetTableByTableType<GenresTable>();
        public FilmsTable FilmsTable => _tablesCollection.GetTableByTableType<FilmsTable>();
        public SeriesTable SeriesTable => _tablesCollection.GetTableByTableType<SeriesTable>();
        public PriorityFilmsTable PriorityFilmsTable => _tablesCollection.GetTableByTableType<PriorityFilmsTable>();
        public BookGenresTable BookGenresTable => _tablesCollection.GetTableByTableType<BookGenresTable>();
        public BooksTable BooksTable => _tablesCollection.GetTableByTableType<BooksTable>();
        public BookCategoriesTable BookCategoriesTable => _tablesCollection.GetTableByTableType<BookCategoriesTable>();
        public PriorityBooksTable PriorityBooksTable => _tablesCollection.GetTableByTableType<PriorityBooksTable>();

        private static TablesCollection GetDefaultTableCollection()
        {
            TablesCollection export = new TablesCollection();

            export.Add(new CategoriesTable());
            export.Add(GenresTable.GetDefaultGenresTable());
            export.Add(new FilmsTable());
            export.Add(new SeriesTable());
            export.Add(new PriorityFilmsTable());
            export.Add(BookGenresTable.GetDefaultGenresTable());
            export.Add(new BooksTable());
            export.Add(new BookCategoriesTable());
            export.Add(new PriorityBooksTable());

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
            {
                string dir = Path.GetDirectoryName(path);
                Directory.CreateDirectory(dir);
                File.Create(path).Dispose();
            }


            return path;
        }

        public void LoadTables(string profileName)
        {
            _tablesCollection.LoadFromFile(CreateProfileFile(profileName));
        }

        public void SaveTables()
        {
            ///_tablesCollection.SaveTable(CreateProfileFile(profileName));
        }

        private void StartSaveTimer()
        {
            if (_saveTimer.Enabled)
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
            //_tablesCollection.SaveTables();
        }
    }
}
