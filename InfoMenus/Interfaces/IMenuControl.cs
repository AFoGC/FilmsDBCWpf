using InfoMenus.MoreInfo;
using InfoMenus.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace InfoMenus.Interfaces
{
    public interface IMenuControl<T> where T : Cell
    {
        T ElementInBuffer { get; set; }
        MoreInfoVisualizer MoreInfoVisualizer { get; }
        UpdateInfoVisualizer UpdateInfoVisualizer { get; }
    }
}
