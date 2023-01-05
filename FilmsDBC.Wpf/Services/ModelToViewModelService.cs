using NewTablesLibrary;
using System.Collections.Specialized;

namespace FilmsDBC.Wpf.Services
{
    public class ModelToViewModelService<T> where T : Cell, new()
    {
        private readonly Table<T> _table;

        public ModelToViewModelService(Table<T> table)
        {
            _table = table;
            _table.CollectionChanged += TableCollectionChanged;
        }

        private void TableCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    OnAddingElement(e.NewItems[0] as T);
                    break;
                case NotifyCollectionChangedAction.Remove:
                    OnRemoveElement(e.OldItems[0] as T);
                    break;
                case NotifyCollectionChangedAction.Move:
                    OnMoivingElement(e.OldStartingIndex, e.NewStartingIndex);
                    break;
                case NotifyCollectionChangedAction.Reset:
                    OnClearing();
                    break;
                default:
                    break;
            }
        }

        protected virtual void OnAddingElement(T element) { }
        protected virtual void OnRemoveElement(T element) { }
        protected virtual void OnMoivingElement(int oldIndex, int newIndex) { }
        protected virtual void OnClearing() { }
    }
}
