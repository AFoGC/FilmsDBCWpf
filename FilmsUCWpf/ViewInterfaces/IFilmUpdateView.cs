using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IFilmUpdateView
    {
        String ID { set; }
        String Name { get; set; }
        IList Genres { get; }
        Genre Genre { get; set; }
        String RealiseYear { get; set; }
        Boolean Wathced { get; set; }
        IList Marks { get; }
        String Mark { get; set; }
        String CountOfViews { get; set; }
        DateTime DateOfWatch { get; set; }
        String Comment { get; set; }

    }
}
