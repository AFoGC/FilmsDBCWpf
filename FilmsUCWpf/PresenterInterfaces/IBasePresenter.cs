using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace FilmsUCWpf.PresenterInterfaces
{
    public interface IBasePresenter
    {
        void AddViewToCollection(IList collection);
        bool SetFindedElement(String search);
        bool HasCheckedProperty(bool isReaded);
        void SetVisualDefault();
        void SetSelectedElement();
    }
}
