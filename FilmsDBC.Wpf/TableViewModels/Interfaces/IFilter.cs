using TL_Objects.Interfaces;

namespace WpfApp.TableViewModels.Interfaces
{
    public interface IFilter
    {
        bool Filter(IGenre[] selectedGenres, bool isReadedChecked, bool isUnReadedChecked);
    }
}
