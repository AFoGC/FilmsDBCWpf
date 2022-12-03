using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Services.Interfaces;

namespace WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly MainWindowModel _model;

        private readonly StatusService _statusService;
        private readonly IExitService _exitService;

        private readonly FilmsViewModel _filmsVM;
        private readonly BooksViewModel _booksVM;
        private readonly SettingsViewModel _settingsVM;

        private bool? _filmsSelected = false;
        private bool? _booksSelected = false;
        private bool? _settingsSelected = false;

        private Visibility _filmsVisibility;
        private Visibility _booksVisibility;
        private Visibility _settingsVisibility;

        private Command _keyDownCommand;
        private Command _saveAndExitCommand;
        private Command _saveSettingsCommand;
        
        private StatusEnum _status;

        public MainViewModel(MainWindowModel model, StatusService statusService, ExitService exitService,
                             FilmsViewModel filmsVM, BooksViewModel booksVM, SettingsViewModel settingsVM)
        {
            _model = model;
            _statusService = statusService;
            _exitService = exitService;

            _filmsVM = filmsVM;
            _booksVM = booksVM;
            _settingsVM = settingsVM;
            
            _statusService.StatusChanged += OnStatusChange;

            FilmsSelected = true;
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
                OnPropertyChanged();
            }
        }

        private void OnStatusChange(StatusEnum status)
        {
            Status = status;
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
                        _model.SaveTables();
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
                    if (_model.IsInfoUnsaved)
                    {
                        CancelEventArgs e = obj as CancelEventArgs;
                        _exitService.ShowDialog();

                        if (_exitService.Save)
                            _model.SaveTables();

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
    }
}
