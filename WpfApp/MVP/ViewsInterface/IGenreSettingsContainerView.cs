using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp.MVP.ViewsInterface
{
    public interface IGenreSettingsContainerView
    {
        IList GenreControls { get; }
        Double Height { get; set; }
        Double DefaultHeight { get; }
    }
}
