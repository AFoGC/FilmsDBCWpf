using System.Collections.Generic;

namespace WpfApp.Factories
{
    public interface IViewCollectionFactory
    {
        IViewCollection CreateViewCollection(object source);
        void SetDescendingProperties(IEnumerable<string> descendingProperties);
    }
}
