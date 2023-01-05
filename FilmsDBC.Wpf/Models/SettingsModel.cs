using System;
using System.Collections.Generic;
using System.Globalization;
using TL_Objects;
using TL_Tables;
using WpfApp.Services;

namespace WpfApp.Models
{
    public class SettingsModel : ISettingsModel
    {
        private readonly SettingsService _settingsService;

        public event Action TablesLoaded;

        public SettingsModel(SettingsService settingsService)
        {
            _settingsService = settingsService;
            TablesService.TablesCollection.TablesLoaded += OnTableLoad;
        }

        private TablesService TablesService => _settingsService.TablesService;
        public IEnumerable<ProfileModel> Profiles => _settingsService.ProfilesService.Profiles;
        public IEnumerable<CultureInfo> Languages => _settingsService.LanguageService.Languages;
        public GenresTable FilmGenresTable => TablesService.FilmGenresTable;
        public BookGenresTable BookGenresTable => TablesService.BookGenresTable;

        public CultureInfo Language
        {
            get => _settingsService.LanguageService.CurrentLanguage;
            set => _settingsService.LanguageService.SetLanguage(value);
        }

        public ScaleEnum Scale
        {
            get => _settingsService.ScaleService.CurrentScale;
            set => _settingsService.ScaleService.SetScale(value);
        }

        //TODO change int to enum
        public int FilmsMarkSystem
        {
            get => TablesService.FilmMarkSystem;
            set => TablesService.FilmMarkSystem = value;
        }

        public int BooksMarkSystem
        {
            get => TablesService.BookMarkSystem;
            set => TablesService.BookMarkSystem = value;
        }

        public double TimerInterval
        {
            get => TablesService.SaveTimerInterval;
            set => TablesService.SaveTimerInterval = value;
        }

        public bool IsTimerEnable
        {
            get => TablesService.IsAutosaveEnable;
            set => TablesService.IsAutosaveEnable = value;
        }

        private void OnTableLoad()
        {
            TablesLoaded?.Invoke();
        }

        public void AddBookGenre()
        {
            BookGenre genre = new BookGenre();
            TablesService.BookGenresTable.Add(genre);
            genre.Name = $"Genre{genre.ID}";
        }

        public void AddFilmGenre()
        {
            Genre genre = new Genre();
            TablesService.FilmGenresTable.Add(genre);
            genre.Name = $"Genre{genre.ID}";
        }

        public bool RemoveBookGenre(BookGenre genre)
        {
            if (genre.Books.Count == 0)
                return TablesService.BookGenresTable.Remove(genre);

            return false;
        }

        public bool RemoveFilmGenre(Genre genre)
        {
            if (genre.Films.Count == 0)
                return TablesService.FilmGenresTable.Remove(genre);

            return false;
        }

        public bool ChangeCheckFilmGenre(Genre genre)
        {
            if (genre.Films.Count == 0)
            {
                genre.IsSerialGenre = !genre.IsSerialGenre;
                return true;
            }

            return false;
        }

        public void SaveSettings()
        {
            _settingsService.SaveSettings();
        }
    }
}
