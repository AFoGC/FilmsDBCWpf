using FilmsUCWpf.PresenterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf
{
    public class PresentersCollection
    {
        public List<IBasePresenter> presenters { get; private set; }
        public int Count { get => presenters.Count; }
        public IBasePresenter this[int index] { get => presenters[index]; }

        public PresentersCollection()
        {
            presenters = new List<IBasePresenter>();
        }

        public void Add(IBasePresenter presenter)
        {
            
        }

        public void Remove()
        {

        }
    }
}
