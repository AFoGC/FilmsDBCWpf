using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using TablesLibrary.Interpreter.TableCell;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface
{
    public interface IControls <ControlMainType, ControlGenreType> : IToUpdateControl where ControlMainType : Cell where ControlGenreType : Cell
    {
        ControlMainType Info { get; }
        bool SetFindedElement(String search);
        void SetVisualDefault();
        void RefreshData();
        bool HasSelectedGenre(ControlGenreType[] selectedGenres);
        bool HasCheckedProperty(bool isReaded);
    }
}
