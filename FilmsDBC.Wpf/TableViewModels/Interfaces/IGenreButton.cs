using TL_Objects.Interfaces;

namespace FilmsDBC.Wpf.TableViewModels.Interfaces
{
    public interface IGenreButton
    {
        bool IsChecked { get; set; }
        IGenre Model { get; }
    }
}
