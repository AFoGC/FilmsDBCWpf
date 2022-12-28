using NewTablesLibrary;

namespace TL_Objects
{
	[Cell("PriorityFilm")]
	public class PriorityFilm : Cell
	{
		[SaveField("film")]
		private readonly OneToOne<PriorityFilm, Film> _film;

		public PriorityFilm()
		{
			_film = new OneToOne<PriorityFilm, Film>(this);
		}

		public Film Film
		{
			get => _film.Value;
			set
			{
                _film.SetValue(value);
				OnPropertyChanged();
			}
		}
	}
}
