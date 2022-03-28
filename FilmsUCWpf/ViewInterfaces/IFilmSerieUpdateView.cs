using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IFilmSerieUpdateView : IFilmUpdateView
    {
        DateTime StartWatchDate { get; set; }
        String CountOfWatchedSeries { get; set; }
        String TotalSeries { get; set; }
    }
}
