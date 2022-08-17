using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;

namespace FilmsUCWpf.ModelBinder
{
    public class BookCategoryBinder : BaseBinder<BookCategory>
    {
        public BookCategoryBinder(BookCategory category) : base(category)
        {
            category.FormatedMark.PropertyChanged += FormatedMark_PropertyChanged;
        }

        private void FormatedMark_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged(nameof(Mark));
            if (e.PropertyName == nameof(Model.FormatedMark.MarkSystem))
            {
                OnPropertyChanged(nameof(Marks));
            }
        }
        
        public String ID
        { 
            get => Model.ID.ToString(); 
            set { } 
        }
        public String Name 
        { 
            get => Model.Name; 
            set => Model.Name = value;
        }
        public String HideName
        {
            get => Model.HideName;
            set => Model.HideName = value;
        }
        public String Mark 
        { 
            get => Model.FormatedMark.ToString(); 
            set => Model.FormatedMark.SetMarkFromString(value);
        }
        public List<String> Marks => Model.FormatedMark.GetComboItems();
    }
}
