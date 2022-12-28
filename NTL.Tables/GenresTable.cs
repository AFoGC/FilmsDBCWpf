using NewTablesLibrary;
using TL_Objects;

namespace TL_Tables
{
    public class GenresTable : Table<Genre>
    {
        public GenresTable()
        {

        }

        public static GenresTable GetDefaultGenresTable()
        {
            GenresTable export = new GenresTable();

            Genre film = new Genre { Name = "film", IsSerialGenre = false };
            Genre series = new Genre { Name = "series", IsSerialGenre = true };

            export.Add(film);
            export.Add(series);

            return export;
        }

        public Genre GetFirstSeriealGenre()
        {
            foreach (Genre genre in this)
                if (genre.IsSerialGenre)
                    return genre;

            return null;
        }
    }
}
