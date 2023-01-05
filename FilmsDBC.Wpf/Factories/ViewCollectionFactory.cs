using System.Collections.Generic;

namespace WpfApp.Factories
{
    public class ViewCollectionFactory : IViewCollectionFactory
    {
        private IEnumerable<string> _descendingProperties;

        public IViewCollection CreateViewCollection(object source)
        {
            ViewCollection viewCollection = new ViewCollection(source, _descendingProperties);

            return viewCollection;
        }

        public void SetDescendingProperties(IEnumerable<string> descendingProperties)
        {
            _descendingProperties = descendingProperties;
        }
    }
}
