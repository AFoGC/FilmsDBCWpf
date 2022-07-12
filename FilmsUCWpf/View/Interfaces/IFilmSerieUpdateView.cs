using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.View.Interfaces
{
    public interface IFilmSerieUpdateView : IFilmUpdateView
    {
        DateTime StartWatchDate { get; set; }
        string CountOfWatchedSeries { get; set; }
        string TotalSeries { get; set; }
    }
}
