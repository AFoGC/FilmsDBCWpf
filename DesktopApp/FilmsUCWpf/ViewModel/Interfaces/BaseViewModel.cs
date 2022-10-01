using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.TableCell;

namespace FilmsUCWpf.ViewModel.Interfaces
{
    public abstract class BaseViewModel<T> : INotifyPropertyChanged, IBaseViewModel where T : Cell
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly DateTime defaultDate = new DateTime();
        private SolidColorBrush textGround;

        public T Model { get; protected set; }
        protected TableCollection TableCollection { get; private set; }
        public SolidColorBrush TextGround
        {
            get => textGround;
            set { textGround = value; OnPropertyChanged(); }
        }

        public BaseViewModel(T model)
        {
            Model = model;
            TableCollection = Model.ParentTable.TableCollection;
        }
        
        protected string FormateDate(DateTime date)
        {
            if (date != defaultDate)
                return date.ToShortDateString();
            else
                return String.Empty;
        }

        protected string formatZero(int import)
        {
            return Cell.FormatToString(import, 0);
        }

        protected int formatEmpty(string import)
        {
            int export = 0;
            if (import != String.Empty)
            {
                Int32.TryParse(import, out export);
            }

            return export;
        }

        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, e);
        }

        protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        public void SetVisualDefault()
        {
            TextGround = BrushColors.DefaultColor;
        }

        public void SetVisualSelected()
        {
            TextGround = BrushColors.SelectColor;
        }

        public void SetVisualFinded()
        {
            TextGround = BrushColors.FindColor;
        }
    }
}
