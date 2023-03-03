using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Windows.Data;

namespace WpfApp.Factories
{
    public class ViewCollection : IViewCollection
    {
        private readonly IEnumerable<string> _descendingProperties;
        private CollectionViewSource _collectionViewSource;

        public ViewCollection(object source, IEnumerable<string> descendingProperties)
        {
            _collectionViewSource = new CollectionViewSource();
            _collectionViewSource.Source = source;

            _descendingProperties = descendingProperties;
        }

        public INotifyCollectionChanged View => _collectionViewSource.View;
        public void ChangeSource(object newSource)
        {
            _collectionViewSource.Source = newSource;
        }

        public void ChangeSortProperty(string propertyName)
        {
            if (_descendingProperties != null)
            {
                if (_descendingProperties.Contains(propertyName))
                {
                    ChangeSortProperty(propertyName, SortDirection.Descending);
                    return;
                }
            }

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
