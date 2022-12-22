using System.Collections.Specialized;

namespace WpfApp.Factories
{
    public class ViewCollectionFactory : IViewCollectionFactory
    {
        public IViewCollection CreateViewCollection(INotifyCollectionChanged source)
        {
            ViewCollection viewCollection = new ViewCollection(source);

            return viewCollection;
        }
    }
}
