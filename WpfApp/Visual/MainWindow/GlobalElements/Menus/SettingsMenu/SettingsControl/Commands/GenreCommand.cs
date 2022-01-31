using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.SettingsMenu.SettingsControl.Commands
{
    public class GenreCommand : ICommand
    {
        private readonly Action _action;
        private readonly Genre _genre;

        public GenreCommand(Action action, Genre genre)
        {
            _action = action;
            _genre = genre;
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public bool CanExecute(object parameter)
        {
            return MainInfo.Tables.FilmsTable.GenreHasFilm(_genre);
        }

        public event EventHandler CanExecuteChanged;
    }
}
