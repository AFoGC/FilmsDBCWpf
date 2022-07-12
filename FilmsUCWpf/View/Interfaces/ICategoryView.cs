using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.View.Interfaces
{
    public interface ICategoryView : IView
    {
        IList CategoryCollection { get; }
        double DefaultHeght { get; }
        double MinimizedHeight { get; }
    }
}
