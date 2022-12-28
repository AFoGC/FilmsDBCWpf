using NewTablesLibrary;
using System.Collections.ObjectModel;
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
