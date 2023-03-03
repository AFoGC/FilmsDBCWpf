using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace TL_Objects.Interfaces
{
    public interface ICategory<T> : ICategory where T : Cell
    {
        ObservableCollection<T> CategoryElements { get; }
    }

    public interface ICategory
    {
        int ID { get; }
        string Name { get; set; }
        string HideName { get; set; }
        int Mark { get; }
        Mark FormatedMark { get; }
    }
}
