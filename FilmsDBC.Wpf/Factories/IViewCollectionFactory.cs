using System.Collections.Generic;
using System.Collections.Specialized;

namespace WpfApp.Factories
{
    public interface IViewCollectionFactory
    {
        IViewCollection CreateViewCollection(object source);
        void SetDescendingProperties(IEnumerable<string> descendingProperties);
    }
}
