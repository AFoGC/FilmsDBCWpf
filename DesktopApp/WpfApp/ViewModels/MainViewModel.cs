using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.Services.Interfaces;
using WpfApp.ViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public class MainViewModel : BaseViewModel, IMainViewModel
    {
        public MainWindowModel Model { get; private set; }

        private StatusEnum _status;
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

        private bool? _filmsSelected = false;
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

        private bool? _booksSelected = false;
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

        private bool? _settingsSelected = false;
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

        private Visibility _filmsVisibility;
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

        private Visibility _booksVisibility;
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

        private Visibility _settingsVisibility;
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

        private Command _keyDownCommand;
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
                        Model.TableCollection.SaveTables();
                    }
                }));
            }
        }

        private IExitService _exitService;
        private Command _saveAndExitCommand;
        public Command SaveAndExitCommand
        {
            get
            {
                return _saveAndExitCommand ??
                (_saveAndExitCommand = new Command(obj =>
                {
                    if (Model.TableCollection.IsInfoUnsaved)
                    {
                        CancelEventArgs e = obj as CancelEventArgs;
                        _exitService.ShowDialog();
                        if (_exitService.Save)
                        {
                            Model.TableCollection.SaveTables();
                        }
                        e.Cancel = !_exitService.Close;
                    }
                }));
            }
        }

        private Command _saveSettingsCommand;
        public Command SaveSettingsCommand
        {
            get
            {
                return _saveSettingsCommand ??
                (_saveSettingsCommand = new Command(obj =>
                {
                    Model.SaveSettings();
                }));
            }
        }

        private Command _minimizeCommand;
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

        private Command _maximizeCommand;
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

        private Command _closeCmmand;
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

        public MainViewModel()
        {
            Model = new MainWindowModel();
            _exitService = new ExitService();

            Model.SaveTimer.Tick += OnTimerSave;
            Model.TableCollection.TableSave += OnSaveStatus;
            Model.TableCollection.CellInTablesChanged += OnTablesChanged;

            FilmsSelected = true;
            Status = StatusEnum.Normal;
        }

        private void OnTimerSave(object sender, EventArgs e)
        {
            Model.TableCollection.SaveTables();
            Model.SaveTimer.Stop();
        }

        private void OnTablesChanged(object sender, EventArgs e)
        {
            Status = StatusEnum.UnSaved;
            if (Model.SaveTimer.IsEnabled)
            {
                Model.SaveTimer.Stop();
                Model.SaveTimer.Start();
            }
        }

        private void OnSaveStatus(object sender, EventArgs e)
        {
            Status = StatusEnum.Saved;
            Model.SaveTimer.Stop();
        }
    }
}
