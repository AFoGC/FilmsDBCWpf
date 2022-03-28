using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IBookCategoryUpdateView
    {
        string ID { set; }
        string Name { get; set; }
    }
}
