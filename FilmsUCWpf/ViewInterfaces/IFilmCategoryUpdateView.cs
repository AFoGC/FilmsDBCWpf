using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IFilmCategoryUpdateView
    {
        String ID { set; }
        String Name { get; set; }
    }
}
