using FilmsUCWpf.PresenterInterfaces;
using FilmsUCWpf.View;
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
	public class BookCategoryPresenter : BasePresenter<BookCategory>, IHasGenre
	{
		protected IMenu<Book> menu;
		private List<BookPresenter> presenters;

		new ICategoryView View { get => (ICategoryView)base.View; }

		public BookCategoryPresenter(BookCategory category, ICategoryView view, IMenu<Book> menu) : base(category, view)
		{
			this.menu = menu;
			presenters = new List<BookPresenter>();
			category.Books.CollectionChanged += Books_CollectionChanged; ;
		}

		private void Books_CollectionChanged(object sender, EventArgs e)
		{
			RefreshCategoryBooks();
		}

		public bool HasSelectedGenre(IGenre[] selectedGenres)
		{
			foreach (BookPresenter presenter in presenters)
			{
				if (presenter.HasSelectedGenre(selectedGenres))
				{
					return true;
				}
			}

			return false;
		}

		private void AddViewPresenter(BookPresenter presenter)
		{
			View.Height += 15;
			View.CategoryCollection.Add(presenter.View);
		}

		private void RefreshCategoryBooks()
		{
			View.CategoryCollection.Clear();
			presenters.Clear();
			View.Height = 20;

			foreach (Book book in Model.Books)
			{
				presenters.Add(new BookPresenter(book, new BookSimpleControl(), menu));
			}

			for (int i = 0; i < presenters.Count; i++)
			{
				BookPresenter presenter = presenters[i];
				if (presenter.Model.FranshiseListIndex != i)
				{
					presenters.Remove(presenter);
					presenters.Insert(presenter.Model.FranshiseListIndex, presenter);
					i = 0;
				}
			}

			foreach (BookPresenter presenter in presenters)
			{
				AddViewPresenter(presenter);
			}
		}

		public bool RemoveBookFromCategory(BookPresenter presenter)
		{
			if (Model.RemoveBookFromCategory(presenter.Model))
			{
				View.CategoryCollection.Remove(presenter.View);
				presenters.Remove(presenter);
				View.Height -= 15;

				foreach (Book book in Model.Books)
				{
					if (presenter.Model.FranshiseListIndex < book.FranshiseListIndex)
					{
						--book.FranshiseListIndex;
					}
				}
				return true;
			}
			return false;
		}

		public override bool HasCheckedProperty(bool isReaded)
		{
			foreach (IBasePresenter presenter in presenters)
			{
				if (presenter.HasCheckedProperty(isReaded))
				{
					return true;
				}
			}

			return false;
		}

		public override bool SetFindedElement(string search)
		{
			bool export = false;
			if (Model.Name.ToLowerInvariant().Contains(search))
			{
				View.SetVisualFinded();
			}

			foreach (BookPresenter presenter in presenters)
			{
				presenter.SetFindedElement(search);
			}

			return export;
		}

		public override void SetSelectedElement()
		{
			View.SetVisualSelected();
		}

		public override void SetVisualDefault()
		{
			View.SetVisualDefault();
			foreach (BookPresenter presenter in presenters)
			{
				presenter.SetVisualDefault();
			}
		}

		public void OpenUpdateMenu()
		{
			//menu.UpdateFormVisualizer.OpenUpdateControl(new FilmUpdateControl());
		}

		public String ID { get => Model.ID.ToString(); set { } }
		public String Name { get => Model.Name; set { } }
	}
}
