using System.Collections.Specialized;

namespace WpfApp.Factories
{
    public interface IViewCollectionFactory
    {
        IViewCollection CreateViewCollection(INotifyCollectionChanged source);
    }
}
