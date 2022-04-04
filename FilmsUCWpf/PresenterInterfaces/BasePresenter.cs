using FilmsUCWpf.ViewInterfaces;
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

namespace FilmsUCWpf.PresenterInterfaces
{
    public abstract class BasePresenter<T> : IBasePresenter, INotifyPropertyChanged where T : Cell
    {
        public T Model { get; protected set; }
        public IView View { get; protected set; }
        protected TableCollection TableCollection { get; private set; }
        public BasePresenter(T model, IView view, TableCollection collection)
        {
            this.Model = model;
            this.View = view;
            this.View.SetPresenter(this);
            this.TableCollection = collection;
            this.Model.CellRemoved += Model_CellRemoved;
            this.Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
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
