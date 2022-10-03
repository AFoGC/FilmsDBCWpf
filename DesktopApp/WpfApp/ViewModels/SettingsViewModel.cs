using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
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
		
		public LangInfo Language
		{
			get => Model.Language;
			set
			{
				Model.Language = value; 
				OnPropertyChanged(); 
			}
		}
		public List<LangInfo> Languages => Model.Cultures;
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
		public GenresTable FilmGenresTable { get; private set; }
		public BookGenresTable BookGenresTable { get; private set; }
		public BooksTable BooksTable { get; private set; }
		public FilmsTable FilmsTable { get; private set; }
		public BookCategoriesTable BookCategoriesTable { get; private set; }
		public CategoriesTable FilmCategoriesTable { get; private set; }

		private Command addBookGenreCommand;
		public Command AddBookGenreCommand
		{
			get
			{
				return addBookGenreCommand ??
				(addBookGenreCommand = new Command(obj =>
				{
					BookGenre genre = new BookGenre();
					BookGenresTable.AddElement(genre);
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
					if (BooksTable.GenreHasBook(genre))
					{
						BookGenresTable.Remove(genre);
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
					FilmGenresTable.AddElement(genre);
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
					if (FilmsTable.GenreHasFilm(genre))
					{
						FilmGenresTable.Remove(genre);
					}
				}));
			}
		}

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
						FilmsTable.MarkSystem = 3;
						FilmCategoriesTable.MarkSystem = 3;
						break;
					case 1:
						FilmsTable.MarkSystem = 5;
						FilmCategoriesTable.MarkSystem = 5;
						break;
					case 2:
						FilmsTable.MarkSystem = 6;
						FilmCategoriesTable.MarkSystem = 6;
						break;
					case 3:
						FilmsTable.MarkSystem = 10;
						FilmCategoriesTable.MarkSystem = 10;
						break;
					case 4:
						FilmsTable.MarkSystem = 12;
						FilmCategoriesTable.MarkSystem = 12;
						break;
					case 5:
						FilmsTable.MarkSystem = 25;
						FilmCategoriesTable.MarkSystem = 25;
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
						BooksTable.MarkSystem = 3;
						BookCategoriesTable.MarkSystem = 3;
						break;
					case 1:
						BooksTable.MarkSystem = 5;
						BookCategoriesTable.MarkSystem = 5;
						break;
					case 2:
						BooksTable.MarkSystem = 6;
						BookCategoriesTable.MarkSystem = 6;
						break;
					case 3:
						BooksTable.MarkSystem = 10;
						BookCategoriesTable.MarkSystem = 10;
						break;
					case 4:
						BooksTable.MarkSystem = 12;
						BookCategoriesTable.MarkSystem = 12;
						break;
					case 5:
						BooksTable.MarkSystem = 25;
						BookCategoriesTable.MarkSystem = 25;
						break;
				}
				OnPropertyChanged();
			}
		}

		public ProfileCollectionModel Profiles => Model.Profiles;
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
					ProfileModel profile = obj as ProfileModel;
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
					ProfileModel profile = obj as ProfileModel;
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
					int i = 1;
					string profName = "import";
					while (Profiles.HasProfileName(profName + i))
					{
						i++;
					}
					profName += i;
					ProfileModel profile = Profiles.AddProfile(profName);

					if (importFileService.OpenFileDialog())
					{
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

			FilmGenresTable = (GenresTable)Model.TableCollection.GetTable<Genre>();
			FilmCategoriesTable = (CategoriesTable)Model.TableCollection.GetTable<Category>();
			FilmsTable = (FilmsTable)Model.TableCollection.GetTable<Film>();

			BookGenresTable = (BookGenresTable)Model.TableCollection.GetTable<BookGenre>();
			BookCategoriesTable = (BookCategoriesTable)Model.TableCollection.GetTable<BookCategory>();
			BooksTable = (BooksTable)Model.TableCollection.GetTable<Book>();

			//Initialize timers list
			Timers = new List<double>();
			Timers.Add(10);
			Timers.Add(15);
			Timers.Add(30);
			Timers.Add(60);
			Timers.Add(360);
			Timers.Add(600);

			//Initialize index of mark systems
			_indexOfFilmMarkSystem = getMarkSystemIndex(FilmsTable);
			_indexOfBookMarkSystem = getMarkSystemIndex(BooksTable);
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
