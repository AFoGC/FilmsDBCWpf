using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Views
{
    public interface ICategoryView<T> : IView<T> where T : Cell 
    {
        UIElementCollection CategoryCollection { get; }
    }
}
