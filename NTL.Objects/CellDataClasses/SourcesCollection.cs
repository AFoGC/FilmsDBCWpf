using NewTablesLibrary;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects.CellDataClasses;

namespace NTL.Objects.CellDataClasses
{
    public class SourcesCollection : ObservableCollection<Source>, ILoadField, ISaveCollection
    {
        public void FromString(string value)
        {
            Source source = Source.ToSource(value);
            Add(source);
        }

        public IEnumerable<string> GetSaveStrings()
        {
            foreach (var item in this)
                yield return item.ToString();
        }
    }
}
