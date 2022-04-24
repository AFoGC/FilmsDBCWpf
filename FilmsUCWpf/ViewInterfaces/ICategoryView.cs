using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface ICategoryView : IView
    {
        IList CategoryCollection { get; }
        Double DefaultHeght { get; }
    }
}
