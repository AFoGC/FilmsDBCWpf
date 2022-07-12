using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public interface IBasePresenter
    {
        void AddViewToCollection(IList collection);
        bool SetFindedElement(string search);
        bool HasCheckedProperty(bool isReaded);
        void SetVisualDefault();
        void SetSelectedElement();
        Cell ModelCell { get; }
        IView View { get; }
    }
}
