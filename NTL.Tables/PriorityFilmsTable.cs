using NewTablesLibrary;
using TL_Objects;

namespace TL_Tables
{
    public class PriorityFilmsTable : Table<PriorityFilm>
    {
        public PriorityFilmsTable()
        {

        }

        public bool ContainsFilm(Film film)
        {
            return this.Any(x => x.Film == film);
        }
    }
}
