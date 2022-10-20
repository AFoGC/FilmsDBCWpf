using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using TL_Objects;
using TL_Tables;
using TL_Tables.Interfaces;
using WpfApp.Commands;
using WpfApp.Models;
using WpfApp.Properties;
using WpfApp.Services;

namespace WpfApp.ViewModels
{
	public class SettingsViewModel : BaseViewModel
	{
		public SettingsModel Model { get; private set; }

		public CultureInfo Language
		{
			get => Model.Language;
			set
			{
				Model.Language = value;
				OnPropertyChanged();
			}
		}
		public List<CultureInfo> Languages => Model.Cultures;
		public List<double> Timers { get; private set; }
		public bool TimerIsEnabled
		{
			get => Model.SaveTimer.IsEnabled;
			set
			{
				Model.SaveTimer.IsEnabled = value;
				OnPropertyChanged();
			}
		}
		public double SelectedTimer
		{
			get => Model.SaveTimer.Interval.TotalSeconds;
			set
			{
				Model.SaveTimer.Interval = TimeSpan.FromSeconds(value);
				OnPropertyChanged();
			}
		}
		public int IndexOfScale
		{
			get => (int)Model.Scale;
			set
			{
				Model.Scale = (ScaleEnum)value;
				OnPropertyChanged();
			}
		}

		public ObservableCollection<Genre> FilmGenres { get; private set; }
		public ObservableCollection<BookGenre> BookGenres { get; private set; }

		private Command addBookGenreCommand;
		public Command AddBookGenreCommand
		{
			get
			{
				return addBookGenreCommand ??
				(addBookGenreCommand = new Command(obj =>
				{
					BookGenre genre = new BookGenre();
					Model.Tables.BookGenresTable.AddElement(genre);
					genre.Name = $"Genre{genre.ID}";
				}));
			}
		}
		private Command deleteBookGenreCommand;
		public Command DeleteBookGenreCommand
		{
			get
			{
				return deleteBookGenreCommand ??
				(deleteBookGenreCommand = new Command(obj =>
				{
					BookGenre genre = obj as BookGenre;
					if (!Model.Tables.BooksTable.GenreHasBook(genre))
					{
						Model.Tables.BookGenresTable.Remove(genre);
					}
				}));
			}
		}

		private Command addFilmGenreCommand;
		public Command AddFilmGenreCommand
		{
			get
			{
				return addFilmGenreCommand ??
				(addFilmGenreCommand = new Command(obj =>
				{
					Genre genre = new Genre();
					Model.Tables.FilmGenresTable.AddElement(genre);
					genre.Name = $"Genre{genre.ID}";
				}));
			}
		}
		private Command deleteFilmGenreCommand;
		public Command DeleteFilmGenreCommand
		{
			get
			{
				return deleteFilmGenreCommand ??
				(deleteFilmGenreCommand = new Command(obj =>
				{
					Genre genre = obj as Genre;
					if (!Model.Tables.FilmsTable.GenreHasFilm(genre))
					{
						Model.Tables.FilmGenresTable.Remove(genre);
					}
				}));
			}
		}
		private Command uncheckFilmGenreCommand;
		public Command UncheckFilmGenreCommand
		{
			get
			{
				return uncheckFilmGenreCommand ??
				(uncheckFilmGenreCommand = new Command(obj =>
				{
					Genre genre = obj as Genre;
					if (Model.Tables.FilmsTable.GenreHasFilm(genre))
					{
						genre.IsSerialGenre = true;
					}
				}));
			}
		}

        public List<String> MarkSystems { get; private set; }

		private int _indexOfFilmMarkSystem;
		public int IndexOfFilmMarkSystem
		{
			get => _indexOfFilmMarkSystem;
			set
			{
				_indexOfFilmMarkSystem = value;
				switch (_indexOfFilmMarkSystem)
				{
					case 0:
                        Model.Tables.FilmsTable.MarkSystem = 3;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 3;
						break;
					case 1:
                        Model.Tables.FilmsTable.MarkSystem = 5;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 5;
						break;
					case 2:
                        Model.Tables.FilmsTable.MarkSystem = 6;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 6;
						break;
					case 3:
                        Model.Tables.FilmsTable.MarkSystem = 10;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 10;
						break;
					case 4:
                        Model.Tables.FilmsTable.MarkSystem = 12;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 12;
						break;
					case 5:
                        Model.Tables.FilmsTable.MarkSystem = 25;
                        Model.Tables.FilmCategoriesTable.MarkSystem = 25;
						break;
				}
				OnPropertyChanged();
			}
		}

		private int _indexOfBookMarkSystem;
		public int IndexOfBookMarkSystem
		{
			get => _indexOfBookMarkSystem;
			set
			{
				_indexOfBookMarkSystem = value;
				switch (_indexOfBookMarkSystem)
				{
					case 0:
                        Model.Tables.BooksTable.MarkSystem = 3;
                        Model.Tables.BookCategoriesTable.MarkSystem = 3;
						break;
					case 1:
                        Model.Tables.BooksTable.MarkSystem = 5;
                        Model.Tables.BookCategoriesTable.MarkSystem = 5;
						break;
					case 2:
                        Model.Tables.BooksTable.MarkSystem = 6;
                        Model.Tables.BookCategoriesTable.MarkSystem = 6;
						break;
					case 3:
                        Model.Tables.BooksTable.MarkSystem = 10;
                        Model.Tables.BookCategoriesTable.MarkSystem = 10;
						break;
					case 4:
                        Model.Tables.BooksTable.MarkSystem = 12;
                        Model.Tables.BookCategoriesTable.MarkSystem = 12;
						break;
					case 5:
                        Model.Tables.BooksTable.MarkSystem = 25;
                        Model.Tables.BookCategoriesTable.MarkSystem = 25;
						break;
				}
				OnPropertyChanged();
			}
		}

		public ProfileCollection Profiles => Model.Profiles;
		private String _newProfileName = String.Empty;
		public String NewProfileName
		{
			get => _newProfileName;
			set { _newProfileName = value; OnPropertyChanged(); }
		}

		private Command changeProfileCommand;
		public Command ChangeProfileCommand
		{
			get
			{
				return changeProfileCommand ??
				(changeProfileCommand = new Command(obj =>
				{
					Profile profile = obj as Profile;
					Profiles.UsedProfile = profile;
				}));
			}
		}

		private Command deleteProfileCommand;
		public Command DeleteProfileCommand
		{
			get
			{
				return deleteProfileCommand ??
				(deleteProfileCommand = new Command(obj =>
				{
					Profile profile = obj as Profile;
					Profiles.RemoveProfile(profile);
				}));
			}
		}

        private static readonly char[] symbols = new char[] { '"', '\\', '/', ':', '|', '<', '>', '*', '?' };
		private IMessageService messageService;
        private Command addProfileCommand;
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
                            Profiles.AddProfile(NewProfileName);
                        }
                    }
					else
					{
						messageService.Show("The following characters are not allowed: \" \\ / : | < > * ? ");
					}
				}));
			}
		}

		private IDialogService importFileService;
		private Command importProfileCommand;
		public Command ImportProfileCommand
		{
			get
			{
				return importProfileCommand ??
				(importProfileCommand = new Command(obj =>
				{
					if (importFileService.OpenFileDialog())
					{
                        int i = 1;
                        string profName = "import";
                        while (Profiles.HasProfileName(profName + i))
                        {
                            i++;
                        }
                        profName += i;
                        Profile profile = Profiles.AddProfile(profName);

                        File.Copy(importFileService.FileName, profile.MainFilePath, true);
                    }
				}));
			}
		}

		private IExplorerService explorerService;
        private Command openExplorerCommand;
        public Command OpenExplorerCommand
        {
            get
            {
                return openExplorerCommand ??
                (openExplorerCommand = new Command(obj =>
                {
                    explorerService.OpenExplorer(Profiles.ProfilesPath);
                }));
            }
        }

        public SettingsViewModel()
		{
			Model = SettingsModel.Initialize();
			importFileService = new ImportFileDialogService();
			messageService = new ShowMessageService();
			explorerService = new ExplorerService();

			FilmGenres = new ObservableCollection<Genre>();
			BookGenres = new ObservableCollection<BookGenre>();
			TablesLoad(Model.TableCollection, null);

			Model.TableCollection.TableLoad += TablesLoad;
			Model.Tables.BookGenresTable.CollectionChanged += BooksChanged;
			Model.Tables.FilmGenresTable.CollectionChanged += FilmsChanged; ;

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
            _indexOfFilmMarkSystem = getMarkSystemIndex(Model.Tables.FilmsTable);
			_indexOfBookMarkSystem = getMarkSystemIndex(Model.Tables.BooksTable);
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

		private void TablesLoad(object sender, EventArgs e)
		{
			FilmGenres.Clear();
			BookGenres.Clear();
			foreach (var genre in Model.Tables.FilmGenresTable) FilmGenres.Add(genre);
			foreach (var genre in Model.Tables.BookGenresTable) BookGenres.Add(genre);
		}

		private int getMarkSystemIndex(IHasMarkSystem table)
		{
			switch (table.MarkSystem)
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
