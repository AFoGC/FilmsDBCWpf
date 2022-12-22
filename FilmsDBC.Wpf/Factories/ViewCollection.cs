using System.Collections.Specialized;
using System.ComponentModel;
using System.Windows.Data;

namespace WpfApp.Factories
{
    public class ViewCollection : IViewCollection
    {
        private CollectionViewSource _collectionViewSource;

        public ViewCollection(INotifyCollectionChanged source)
        {
            _collectionViewSource = new CollectionViewSource();
            _collectionViewSource.Source = source;
        }

        public INotifyCollectionChanged View => _collectionViewSource.View;
        public void ChangeSource(object newSource)
        {
            _collectionViewSource.Source = newSource;
        }

        public void ChangeSortProperty(string propertyName)
        {
            ChangeSortProperty(propertyName, SortDirection.Ascending);
        }

        public void ChangeSortProperty(string propertyName, SortDirection direction)
        {
            ListSortDirection sortDirection = (ListSortDirection)direction;

            _collectionViewSource.SortDescriptions.Clear();
            _collectionViewSource.SortDescriptions.Add(new SortDescription(propertyName, sortDirection));
        }

        public void RemoveSort()
        {
            _collectionViewSource.SortDescriptions.Clear();
        }
    }
}
