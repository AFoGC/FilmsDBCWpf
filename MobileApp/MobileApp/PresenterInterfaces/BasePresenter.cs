using MobileApp.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace MobileApp.PresenterInterfaces
{
    public abstract class BasePresenter<T> : IBasePresenter where T : Cell
    {
        public T Model { get; protected set; }
        public IView View { get; protected set; }
        public BasePresenter(T model, IView view)
        {
            this.Model = model;
            this.View = view;
            this.Model.CellRemoved += Model_CellRemoved;
        }

        private void Model_CellRemoved(object sender, EventArgs e)
        {
            View.SelfRemove();
        }

        public abstract bool HasCheckedProperty(bool isReaded);
        public abstract bool SetFindedElement(string search);
        public abstract void SetSelectedElement();
        public abstract void SetVisualDefault();

        public void AddViewToCollection(IList collection)
        {
            collection.Add(View);
        }

        public Cell ModelCell { get => Model; }
    }
}
