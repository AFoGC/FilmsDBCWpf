using System.Collections.Specialized;

namespace WpfApp.Factories
{
    public interface IViewCollection
    {
        public INotifyCollectionChanged View { get; }
        public void ChangeSource(object newSource);
        public void ChangeSortProperty(string propertyName);
        public void ChangeSortProperty(string propertyName, SortDirection direction);
        public void RemoveSort();
    }
}
