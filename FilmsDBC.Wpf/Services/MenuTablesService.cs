using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using WpfApp.Factories;
using WpfApp.TableViewModels.Interfaces;

namespace FilmsDBC.Wpf.Services
{
    public class MenuTablesService
    {
        private readonly Dictionary<string, (IEnumerable, IViewCollection)> _tables;
        private readonly IViewCollectionFactory _viewCollectionFactory;

        public MenuTablesService(IViewCollectionFactory factory)
        {
            _tables = new Dictionary<string, (IEnumerable, IViewCollection)>();
            _viewCollectionFactory = factory;
        }

        public void Add(string name, IEnumerable table)
        {
            IViewCollection collectionView = _viewCollectionFactory.CreateViewCollection(table);
            _tables.Add(name, (table, collectionView));
        }

        public IEnumerable GetTable(string name)
        {
            return _tables.Where(x => x.Key == name).First().Value.Item1;
        }

        public IViewCollection GetViewCollecton(string name)
        {
            return _tables.Where(x => x.Key == name).First().Value.Item2;
        }

        public void ChangeSortProperty(string propertyName, params string[] tablesNames)
        {
            IEnumerable<IViewCollection> currentViewCollections = _tables
                .Where(x => tablesNames.Contains(x.Key))
                .Select(x => x.Value.Item2);

            foreach (IViewCollection viewCollection in currentViewCollections)
                viewCollection.ChangeSortProperty(propertyName);
        }

        public void SearchInTables(string searchText)
        {
            searchText = searchText.ToLower();

            foreach (IEnumerable table in _tables.Values.Select(x => x.Item1))
                foreach (IFinded vm in table)
                    vm.SetFinded(searchText);
        }

        //public void Filter
    }
}
