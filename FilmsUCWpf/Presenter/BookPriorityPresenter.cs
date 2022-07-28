using FilmsUCWpf.Presenter.Interfaces;
using FilmsUCWpf.View.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.Presenter
{
    public class BookPriorityPresenter : BookPresenter
    {
        public PriorityBook PriorityModel { get; protected set; }
        public BookPriorityPresenter(PriorityBook priorityBook, IView view, IMenuPresenter<Book> menu, TableCollection tableCollection) : base(priorityBook.Book, view, menu, tableCollection)
        {
            this.PriorityModel = priorityBook;
            PriorityModel.CellRemoved += PriorityModel_CellRemoved;
        }

        private void PriorityModel_CellRemoved(object sender, EventArgs e)
        {
            View.SelfRemove();
        }

        public void RemoveFromPriority()
        {
            PriorityBooksTable priorityBooks = (PriorityBooksTable)TableCollection.GetTable<PriorityBook>();
            priorityBooks.Remove(PriorityModel);
        }
    }
}
