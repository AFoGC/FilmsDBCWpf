using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.ViewsInterface
{
    public interface IGenreSettingsContainerView
    {
        IList GenreControls { get; }
        double Height { get; set; }
        double DefaultHeight { get; }
    }
}
