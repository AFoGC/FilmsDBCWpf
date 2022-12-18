using TL_Objects.Interfaces;

namespace WpfApp.TableViewModels.Interfaces
{
    public interface IHasGenre
    {
        bool HasSelectedGenre(IGenre[] selectedGenres);
    }
}
