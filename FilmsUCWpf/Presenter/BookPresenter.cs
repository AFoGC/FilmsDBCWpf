using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using TL_Objects.Interfaces;

namespace FilmsUCWpf.Presenter
{
    public class BookPresenter : BasePresenter<Book>, IHasGenre
    {
        protected IMenu<Book> menu;
        public BookPresenter(Book book, IBaseView view, IMenu<Book> menu) : base(book, view)
        {
            this.menu = menu;
            book.BookGenre.PropertyChanged += BookGenre_PropertyChanged;
        }

        private void BookGenre_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            OnPropertyChanged("BookGenre");
        }

        public override bool HasCheckedProperty(bool isReaded)
        {
            return isReaded == Model.Readed;
        }

        public override bool SetFindedElement(string search)
        {
            if (this.Model.Name.ToLowerInvariant().Contains(search))
            {
                View.SetVisualFinded();
                return true;
            }

            return false;
        }

        public override void SetSelectedElement()
        {
            if (menu.ControlInBuffer != null)
            {
                menu.ControlInBuffer = this;
                View.SetVisualSelected();
            }
        }

        public override void SetVisualDefault()
        {
            View.SetVisualDefault();
        }

        public bool HasSelectedGenre(IGenre[] selectedGenres)
        {
            foreach (IGenre genre in selectedGenres)
            {
                if (genre == Model.BookGenre)
                {
                    return true;
                }
            }
            return false;
        }

        public void CopyUrl()
        {
            Helper.CopyFirstSource(Model.Sources);
        }

        public void OpenUpdateMenu()
        {
            throw new NotImplementedException();
        }

        public void OpenInfoMenu()
        {
            throw new NotImplementedException();
        }

        private static Book defBook = new Book();
        public String ID { get => Model.ID.ToString(); set { } }
        public String Name { get => Model.Name; set { } }
        public String Genre { get => Model.BookGenre.ToString(); set { } }
        public String PublicationYear { get => Film.FormatToString(Model.PublicationYear, defBook.PublicationYear); set { } }
        public Boolean Readed { get => Model.Readed; set { } }
        public String Author { get => Model.Author; set { } }
        public String FullReadDate { get => Book.FormatToString(Model.FullReadDate, defBook.FullReadDate); set { } }
        public String Mark { get => Helper.MarkToText(Book.FormatToString(Model.Mark, defBook.Mark)); set { } }
        public String CountOfReadings { get => Book.FormatToString(Model.CountOfReadings, defBook.CountOfReadings); set { } }
        public String Bookmark { get => Model.Bookmark; set { } }
        public String Sources { get => Helper.SourcesStateString(Model.Sources); set { } }
    }
}
