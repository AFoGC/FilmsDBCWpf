using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.Presenters
{
    public interface IBasePresenter
    {
        bool SetFindedElement(String search);
        bool HasCheckedProperty(bool isReaded);
    }
}
