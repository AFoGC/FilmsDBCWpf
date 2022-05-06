using System;
using System.Collections;
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
        String HideName { get; set; }
        String Mark { get; set; }
        IList Marks { get; }
    }
}
