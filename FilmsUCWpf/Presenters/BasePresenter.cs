using FilmsUCWpf.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.Presenters
{
    public abstract class BasePresenter<T> : IBasePresenter where T : Cell
    {
        public T Model { get; protected set; }
        public IView<T> View { get; internal set; }

        public BasePresenter(T model)
        {
            this.Model = model;
            this.Model.CellRemoved += Model_CellRemoved;
        }

        private void Model_CellRemoved(object sender, EventArgs e)
        {
            View.SelfRemove();
        }

        public abstract bool SetFindedElement(String search);
        public abstract bool HasCheckedProperty(bool isReaded);
    }
}
