using System.Collections;
using TL_Objects.Interfaces;
using WpfApp.TableViewModels.Interfaces;

namespace FilmsDBC.Wpf.Services
{
    public class MenuRequestPanelService
    {
        private readonly IEnumerable[] _viewModelTables;

        public MenuRequestPanelService(params IEnumerable[] viewModelTables)
        {
            _viewModelTables = viewModelTables;
        }

        public bool IsWatchedCheck { get; set; }
        public bool IsUnWatchedCheck { get; set; }

        public void SearchInTables(string searchText)
        {
            foreach (IEnumerable table in _viewModelTables)
                foreach (IFinded viewModel in table)
                    viewModel.SetFinded(searchText);
        }

        public void FilterInTables(IGenre[] selectedGenres)
        {
            foreach (IEnumerable table in _viewModelTables)
                foreach (IFilter viewModel in table)
                    viewModel.Filter(selectedGenres, IsWatchedCheck, IsUnWatchedCheck);
        }
    }
}
