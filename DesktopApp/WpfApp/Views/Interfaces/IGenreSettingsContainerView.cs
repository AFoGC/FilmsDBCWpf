using System.Collections;

namespace WpfApp.Views.Interfaces
{
    public interface IGenreSettingsContainerView
    {
        IList GenreControls { get; }
        double Height { get; set; }
        double DefaultHeight { get; }
    }
}
