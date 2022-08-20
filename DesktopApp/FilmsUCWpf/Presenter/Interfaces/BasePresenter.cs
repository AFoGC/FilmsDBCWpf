using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenter.Interfaces
{
    public abstract class BasePresenter<T> : IBasePresenter where T : Cell
    {
        public T Model { get; protected set; }
        public IView View { get; protected set; }
        protected TableCollection TableCollection { get; private set; }
        public BasePresenter(T model, IView view, TableCollection collection)
        {
            Model = model;
            View = view;
            View.SetPresenter(this);
            TableCollection = collection;
            Model.CellRemoved += Model_CellRemoved;
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
