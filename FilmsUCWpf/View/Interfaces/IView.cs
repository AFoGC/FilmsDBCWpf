using FilmsUCWpf.Presenter.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.View.Interfaces
{
    public interface IView
    {
        IBasePresenter Presenter { get; }
        bool SetPresenter(IBasePresenter presenter);
        void SetVisualDefault();
        void SetVisualSelected();
        void SetVisualFinded();
        void SelfRemove();
        double Height { get; set; }
    }
}
