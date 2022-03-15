using InfoMenusWpf.MoreInfo;
using InfoMenusWpf.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenters
{
    public interface IMenu<T> where T : Cell
    {
        BasePresenter<T> ControlInBuffer { get; set; }
        MoreInfoFormVisualizer MoreInfoFormVisualizer { get; }
        UpdateFormVisualizer UpdateFormVisualizer { get; }
    }
}
