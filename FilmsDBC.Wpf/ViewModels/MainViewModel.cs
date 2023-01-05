using System.ComponentModel;
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

        private RelayCommand _keyDownCommand;
        private RelayCommand _saveAndExitCommand;
        private RelayCommand _saveSettingsCommand;

        private object _currentViewModel;
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
                    CurrentViewModel = FilmsVM;
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
                    CurrentViewModel = BooksVM;
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
                    CurrentViewModel = SettingsVM;
                }
                OnPropertyChanged();
            }
        }

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand KeyDownCommand
        {
            get
            {
                return _keyDownCommand ??
                (_keyDownCommand = new RelayCommand(obj =>
                {
                    KeyEventArgs e = obj as KeyEventArgs;
                    if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
                    {
                        _model.SaveTables();
                    }
                }));
            }
        }

        public RelayCommand SaveAndExitCommand
        {
            get
            {
                return _saveAndExitCommand ??
                (_saveAndExitCommand = new RelayCommand(obj =>
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

        public RelayCommand SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ??
                (_saveSettingsCommand = new RelayCommand(obj =>
                {
                    _model.SaveSettings();
                }));
            }
        }
    }
}
