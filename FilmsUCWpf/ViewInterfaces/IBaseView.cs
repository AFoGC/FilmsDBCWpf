using FilmsUCWpf.PresenterInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsUCWpf.ViewInterfaces
{
    public interface IBaseView
    {
        bool SetPresenter(IBasePresenter presenter);
        void SetVisualDefault();
        void SetVisualSelected();
        void SetVisualFinded();
        void SelfRemove();
        double Height { get; set; }
    }
}
