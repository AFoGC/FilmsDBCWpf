using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Views
{
    public interface IView<T> where T : Cell
    {
        T Info { get; }
        void SetVisualDefault();
        void SetVisualSelected();
        void SetVisualFinded();
        void SelfRemove();
    }
}
