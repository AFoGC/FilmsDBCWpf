using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.View.Interfaces
{
    public interface IFilmUpdateView
    {
        string ID { set; }
        string Name { get; set; }
        IList Genres { get; }
        Genre Genre { get; set; }
        string RealiseYear { get; set; }
        bool Wathced { get; set; }
        IList Marks { get; }
        string Mark { get; set; }
        string CountOfViews { get; set; }
        DateTime DateOfWatch { get; set; }
        string Comment { get; set; }

    }
}
