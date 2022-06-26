using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.ModelBinder
{
    public abstract class BaseBinder<T> : INotifyPropertyChanged where T : Cell
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public T Model { get; private set; }
        public BaseBinder(T model)
        {
            Model = model;
            Model.PropertyChanged += Model_PropertyChanged;
        }

        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

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

        private DateTime defaultDate = new DateTime();
        protected string FormateDate(DateTime date)
        {
            if (date != defaultDate)
                return date.ToShortDateString();
            else
                return String.Empty;
        }
    }
}
