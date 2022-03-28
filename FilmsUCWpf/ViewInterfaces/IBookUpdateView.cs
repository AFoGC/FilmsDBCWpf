using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IBookUpdateView
    {
        string ID { set; }
        string Name { get; set; }
        BookGenre Genre { get; set; }
        string RealiseYear { get; set; }
        bool Readed { get; set; }
        string Author { get; set; }
        DateTime FullReadDate { get; set; }
        string Mark { get; set; }
        string CountOfReadings { get; set; }
        string Bookmark { get; set; }
    }
}
