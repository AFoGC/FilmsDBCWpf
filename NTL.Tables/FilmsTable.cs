using NewTablesLibrary;
using System.Collections.Specialized;
using TL_Objects;
using TL_Tables.Interfaces;

namespace TL_Tables
{
	public class FilmsTable : Table<Film>, IHasMarkSystem
	{
        [SaveField("markSystem")]
        private int _markSystem;

        [SaveField("newMarkSystem")]
        private bool _newMarkSystem;

		public FilmsTable()
        {
			_newMarkSystem = false;
			MarkSystem = 6;
        }

        public bool NewMarkSystem => _newMarkSystem;
        public int MarkSystem
        {
            get => _markSystem;
            set
            {
                _markSystem = value;
                foreach (Film film in this)
                {
                    film.FormatedMark.MarkSystem = _markSystem;
                }
            }
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                Film film = (Film)e.NewItems[0];
                film.FormatedMark.MarkSystem = MarkSystem;
            }

            base.OnCollectionChanged(e);
        }

        protected override void OnLoaded()
        {
            MarkSystem = _markSystem;

            if (NewMarkSystem == false)
                ChangeToNewMarkSystem();

            base.OnLoaded();
        }

		private void ChangeToNewMarkSystem()
        {
			foreach (Film film in this)
			{
				film.Mark *= 50;
			}

		    _newMarkSystem = true;
		}
	}
}
