using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter.TableCell;

namespace MobileApp.PresenterInterfaces
{
    public interface IBasePresenter
    {
        void AddViewToCollection(IList collection);
        bool SetFindedElement(String search);
        bool HasCheckedProperty(bool isReaded);
        void SetVisualDefault();
        void SetSelectedElement();
        Cell ModelCell { get; }
    }
}
