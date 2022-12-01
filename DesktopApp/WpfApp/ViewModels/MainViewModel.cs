using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Services.Interfaces;
using WpfApp.ViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        private readonly MainWindowModel _model;

        private readonly FilmsViewModel _filmsVM;
        private readonly BooksViewModel _booksVM;
        private readonly SettingsViewModel _settingsVM;

        private readonly IExitService _exitService;

        private bool? _filmsSelected = false;
        private bool? _booksSelected = false;
        private bool? _settingsSelected = false;

        private Visibility _filmsVisibility;
        private Visibility _booksVisibility;
        private Visibility _settingsVisibility;

        private Command _keyDownCommand;
        private Command _saveAndExitCommand;
        private Command _saveSettingsCommand;
        private Command _minimizeCommand;
        private Command _maximizeCommand;
        private Command _closeCmmand;
        
        private StatusEnum _status;

        public MainViewModel(MainWindowModel model, FilmsViewModel filmsVM, BooksViewModel booksVM, SettingsViewModel settingsVM)
        {
            SettingsService settingsService = CreateSettingsService();
            _model = model;

            _filmsVM = filmsVM;
            _booksVM = booksVM;
            _settingsVM = settingsVM;

            _exitService = new ExitService();

            _model.TableCollection.TableSave += OnSaveStatus;
            _model.TableCollection.CellInTablesChanged += OnTablesChanged;

            FilmsSelected = true;
            Status = StatusEnum.Normal;
        }

        public FilmsViewModel FilmsVM => _filmsVM;
        public BooksViewModel BooksVM => _booksVM;
        public SettingsViewModel SettingsVM => _settingsVM;
        

        public StatusEnum Status
        {
            get => _status;
            set
            { 
                _status = value;
                if (value == StatusEnum.Saved)
                {
                    new StatusTimer(value, this);
                }
                
                OnPropertyChanged();
            }
        }
        
        public bool? FilmsSelected
        {
            get => _filmsSelected;
            set
            {
                _filmsSelected = value;
                if (_filmsSelected == true)
                {
                    BooksSelected = false;
                    SettingsSelected = false;
                    FilmsVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }
        
        public bool? BooksSelected
        {
            get => _booksSelected;
            set
            {
                _booksSelected = value;
                if (_booksSelected == true)
                {
                    FilmsSelected = false;
                    SettingsSelected = false;
                    BooksVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }
        
        public bool? SettingsSelected
        {
            get => _settingsSelected;
            set
            {
                _settingsSelected = value;
                if (_settingsSelected == true)
                {
                    FilmsSelected = false;
                    BooksSelected = false;
                    SettingsVisibility = Visibility.Visible;
                }
                OnPropertyChanged();
            }
        }
        
        public Visibility FilmsVisibility
        {
            get => _filmsVisibility;
            set
            {
                _filmsVisibility = value;
                if (_filmsVisibility == Visibility.Visible)
                {
                    BooksVisibility = Visibility.Hidden;
                    SettingsVisibility = Visibility.Hidden;
                }
                OnPropertyChanged();
            }
        }
        
        public Visibility BooksVisibility
        {
            get => _booksVisibility;
            set
            {
                _booksVisibility = value;
                if (_booksVisibility == Visibility.Visible)
                {
                    FilmsVisibility = Visibility.Hidden;
                    SettingsVisibility = Visibility.Hidden;
                }
                OnPropertyChanged();
            }
        }
        
        public Visibility SettingsVisibility
        {
            get => _settingsVisibility;
            set
            {
                _settingsVisibility = value;
                if (_settingsVisibility == Visibility.Visible)
                {
                    FilmsVisibility = Visibility.Hidden;
                    BooksVisibility = Visibility.Hidden;
                }
                OnPropertyChanged();
            }
        }
        
        public Command KeyDownCommand
        {
            get
            {
                return _keyDownCommand ??
                (_keyDownCommand = new Command(obj =>
                {
                    KeyEventArgs e = obj as KeyEventArgs;
                    if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        _model.TableCollection.SaveTables();
                    }
                }));
            }
        }
        
        public Command SaveAndExitCommand
        {
            get
            {
                return _saveAndExitCommand ??
                (_saveAndExitCommand = new Command(obj =>
                {
                    if (_model.TableCollection.IsInfoUnsaved)
                    {
                        CancelEventArgs e = obj as CancelEventArgs;
                        _exitService.ShowDialog();
                        if (_exitService.Save)
                        {
                            _model.TableCollection.SaveTables();
                        }
                        e.Cancel = !_exitService.Close;
                    }
                }));
            }
        }
        
        public Command SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ??
                (_saveSettingsCommand = new Command(obj =>
                {
                    _model.SaveSettings();
                }));
            }
        }
        
        public Command MinimizeCommand
        {
            get
            {
                return _minimizeCommand ??
                (_minimizeCommand = new Command(obj =>
                {
                    Application.Current.MainWindow.WindowState = WindowState.Minimized;
                }));
            }
        }
        
        public Command MaximizeCommand
        {
            get
            {
                return _maximizeCommand ??
                (_maximizeCommand = new Command(obj =>
                {
                    if (App.Current.MainWindow.WindowState != WindowState.Maximized)
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Maximized;
                    }
                    else
                    {
                        Application.Current.MainWindow.WindowState = WindowState.Normal;
                    }
                }));
            }
        }
        
        public Command CloseCmmand
        {
            get
            {
                return _closeCmmand ??
                (_closeCmmand = new Command(obj =>
                {
                    App.Current.MainWindow.Close();
                }));
            }
        }

        private SettingsService CreateSettingsService()
        {
            TablesFileService tablesService = new TablesFileService();
            LanguageService languageService = new LanguageService();
            ProfilesService profilesService = new ProfilesService();
            ScaleService scaleService = new ScaleService();

            return new SettingsService(tablesService, languageService,
                                        profilesService, scaleService);
        }

        private void OnTablesChanged(object sender, EventArgs e)
        {
            Status = StatusEnum.UnSaved;
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            Status = StatusEnum.Saved;
        }
    }
}
