using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsDBC.Wpf.Services
{
    public class MenuTablesService : IEnumerable<IEnumerable>
    {
        private readonly List<IEnumerable> _tables;

        public MenuTablesService()
        {

        }





        public IEnumerator<IEnumerable> GetEnumerator()
        {
            return _tables.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
