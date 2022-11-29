using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using TL_Objects;
using WpfApp.Commands;
using WpfApp.Helper;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Services.Interfaces;

namespace WpfApp.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private readonly ProfilesModel _profilesModel;
        private readonly SettingsModel _model;

        private IMessageService messageService;
        private IDialogService importFileService;
        private IExplorerService explorerService;

        private int _indexOfFilmMarkSystem;
        private int _indexOfBookMarkSystem;
        private String _newProfileName = String.Empty;

        private Command addBookGenreCommand;
        private Command deleteBookGenreCommand;
        private Command addFilmGenreCommand;
        private Command deleteFilmGenreCommand;
        private Command uncheckFilmGenreCommand;
        private Command changeProfileCommand;
        private Command deleteProfileCommand;
        private Command importProfileCommand;
        private Command addProfileCommand;
        private Command openExplorerCommand;

        private static readonly char[] symbols = new char[] 
        { '"', '\\', '/', ':', '|', '<', '>', '*', '?' };

        public SettingsViewModel(SettingsService settingsService)
        {
            _model = new SettingsModel(settingsService);
            _profilesModel = new ProfilesModel(settingsService.TablesService, 
                                               settingsService.ProfilesService);

            importFileService = new ImportFileDialogService();
            messageService = new ShowMessageService();
            explorerService = new ExplorerService();

            FilmGenres = new ObservableCollection<Genre>();
            BookGenres = new ObservableCollection<BookGenre>();
            OnTablesLoad();

            _model.TablesLoaded += OnTablesLoad;
            _model.BookGenresTable.CollectionChanged += BooksChanged;
            _model.FilmGenresTable.CollectionChanged += FilmsChanged;

            //Initialize timers list
            Timers = new List<double>();
            Timers.Add(10);
            Timers.Add(15);
            Timers.Add(30);
            Timers.Add(60);
            Timers.Add(360);
            Timers.Add(600);

            //Initialize mark systems list
            MarkSystems = new List<string>();
            MarkSystems.Add("3/3");
            MarkSystems.Add("5/5");
            MarkSystems.Add("6/6");
            MarkSystems.Add("10/10");
            MarkSystems.Add("12/12");
            MarkSystems.Add("25/25");

            //Initialize index of mark systems
            _indexOfFilmMarkSystem = getMarkSystemIndex(_model.FilmsMarkSystem);
            _indexOfBookMarkSystem = getMarkSystemIndex(_model.BooksMarkSystem);

            _model.LoadSettings();
        }

        public ObservableCollection<Genre> FilmGenres { get; private set; }
        public ObservableCollection<BookGenre> BookGenres { get; private set; }
        public List<double> Timers { get; private set; }
        public List<String> MarkSystems { get; private set; }
        public IEnumerable<ProfileModel> Profiles => _model.Profiles;
        public IEnumerable<CultureInfo> Languages => _model.Languages;

        public CultureInfo Language
        {
            get => _model.Language;
            set
            {
                _model.Language = value;
                OnPropertyChanged();
            }
        }
        
        public bool TimerIsEnabled
        {
            get => _model.IsTimerEnable;
            set
            {
                _model.IsTimerEnable = value;
                OnPropertyChanged();
            }
        }

        public double SelectedTimer
        {
            get => _model.TimerInterval;
            set
            {
                _model.TimerInterval = value;
                OnPropertyChanged();
            }
        }

        public int IndexOfScale
        {
            get => (int)_model.Scale;
            set
            {
                _model.Scale = (ScaleEnum)value;
                OnPropertyChanged();
            }
        }
        
        public Command AddBookGenreCommand
        {
            get
            {
                return addBookGenreCommand ??
                (addBookGenreCommand = new Command(obj =>
                {
                    _model.AddBookGenre();
                }));
            }
        }
        
        public Command DeleteBookGenreCommand
        {
            get
            {
                return deleteBookGenreCommand ??
                (deleteBookGenreCommand = new Command(obj =>
                {
                    _model.RemoveBookGenre(obj as BookGenre);
                }));
            }
        }
        
        public Command AddFilmGenreCommand
        {
            get
            {
                return addFilmGenreCommand ??
                (addFilmGenreCommand = new Command(obj =>
                {
                    _model.AddFilmGenre();
                }));
            }
        }
        
        public Command DeleteFilmGenreCommand
        {
            get
            {
                return deleteFilmGenreCommand ??
                (deleteFilmGenreCommand = new Command(obj =>
                {
                    _model.RemoveFilmGenre(obj as Genre);
                }));
            }
        }
        
        //TODO change Method Name and change call of the command in view 
        public Command UncheckFilmGenreCommand
        {
            get
            {
                return uncheckFilmGenreCommand ??
                (uncheckFilmGenreCommand = new Command(obj =>
                {
                    _model.ChangeCheckFilmGenre(obj as Genre);
                }));
            }
        }
        
        public int IndexOfFilmMarkSystem
        {
            get => _indexOfFilmMarkSystem;
            set
            {
                _indexOfFilmMarkSystem = value;
                switch (_indexOfFilmMarkSystem)
                {
                    case 0:
                        _model.FilmsMarkSystem = 3;
                        break;
                    case 1:
                        _model.FilmsMarkSystem = 5;
                        break;
                    case 2:
                        _model.FilmsMarkSystem = 6;
                        break;
                    case 3:
                        _model.FilmsMarkSystem = 10;
                        break;
                    case 4:
                        _model.FilmsMarkSystem = 12;
                        break;
                    case 5:
                        _model.FilmsMarkSystem = 25;
                        break;
                }
                OnPropertyChanged();
            }
        }
        
        public int IndexOfBookMarkSystem
        {
            get => _indexOfBookMarkSystem;
            set
            {
                _indexOfBookMarkSystem = value;
                switch (_indexOfBookMarkSystem)
                {
                    case 0:
                        _model.BooksMarkSystem = 3;
                        break;
                    case 1:
                        _model.BooksMarkSystem = 5;
                        break;
                    case 2:
                        _model.BooksMarkSystem = 6;
                        break;
                    case 3:
                        _model.BooksMarkSystem = 10;
                        break;
                    case 4:
                        _model.BooksMarkSystem = 12;
                        break;
                    case 5:
                        _model.BooksMarkSystem = 25;
                        break;
                }
                OnPropertyChanged();
            }
        }
        
        public String NewProfileName
        {
            get => _newProfileName;
            set { _newProfileName = value; OnPropertyChanged(); }
        }
        
        public Command ChangeProfileCommand
        {
            get
            {
                return changeProfileCommand ??
                (changeProfileCommand = new Command(obj =>
                {
                    _profilesModel.SetUsedProfile(obj as ProfileModel);
                }));
            }
        }
        
        public Command DeleteProfileCommand
        {
            get
            {
                return deleteProfileCommand ??
                (deleteProfileCommand = new Command(obj =>
                {
                    _profilesModel.RemoveProfile(obj as ProfileModel);
                }));
            }
        }
        
        public Command AddProfileCommand
        {
            get
            {
                return addProfileCommand ??
                (addProfileCommand = new Command(obj =>
                {
                    if (NewProfileName.IndexOfAny(symbols) == -1)
                    {
                        if (NewProfileName != String.Empty)
                        {
                            _profilesModel.AddProfile(NewProfileName);
                        }
                    }
                    else
                    {
                        messageService.Show("The following characters are not allowed: \" \\ / : | < > * ? ");
                    }
                }));
            }
        }
        
        public Command ImportProfileCommand
        {
            get
            {
                return importProfileCommand ??
                (importProfileCommand = new Command(obj =>
                {
                    if (importFileService.OpenFileDialog())
                    {
                        _profilesModel.ImportProfile(importFileService.FileName);
                    }
                }));
            }
        }
        
        public Command OpenExplorerCommand
        {
            get
            {
                return openExplorerCommand ??
                (openExplorerCommand = new Command(obj =>
                {
                    explorerService.OpenExplorer(PathHelper.ProfilesPath);
                }));
            }
        }

        private void FilmsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            Genre genre;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    genre = e.NewItems[0] as Genre;
                    FilmGenres.Add(genre);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    genre = e.OldItems[0] as Genre;
                    FilmGenres.Remove(genre);
                    break;
                case NotifyCollectionChangedAction.Move:
                    FilmGenres.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    FilmGenres.Clear();
                    break;
            }
        }

        private void BooksChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            BookGenre genre;
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    genre = e.NewItems[0] as BookGenre;
                    BookGenres.Add(genre);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    genre = e.OldItems[0] as BookGenre;
                    BookGenres.Remove(genre);
                    break;
                case NotifyCollectionChangedAction.Move:
                    BookGenres.Move(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    BookGenres.Clear();
                    break;
            }
        }

        private void OnTablesLoad()
        {
            FilmGenres.Clear();
            BookGenres.Clear();
            foreach (var genre in _model.FilmGenresTable) FilmGenres.Add(genre);
            foreach (var genre in _model.BookGenresTable) BookGenres.Add(genre);
        }

        private int getMarkSystemIndex(int markSystem)
        {
            switch (markSystem)
            {
                case 3:
                    return 0;
                case 5:
                    return 1;
                case 6:
                    return 2;
                case 10:
                    return 3;
                case 12:
                    return 4;
                case 25:
                    return 5;

                default:
                    return 0;
            }
        }
    }
}
