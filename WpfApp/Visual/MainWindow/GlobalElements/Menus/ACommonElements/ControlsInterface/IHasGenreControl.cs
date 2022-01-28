using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface
{
    public interface IHasGenreControl<ControlGenreType> : IBaseControls where ControlGenreType : Cell
    {
        bool HasSelectedGenre(ControlGenreType[] selectedGenres);
    }
}
