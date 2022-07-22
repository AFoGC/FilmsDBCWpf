using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TL_Objects;
using TL_Tables;

namespace FilmsUCWpf.ModelBinder
{
	public class BookSimpleBinder : BookBinder
	{
		private BookCategoriesTable bookCategories;
		public BookSimpleBinder(Book book) : base(book)
		{
			TableCollection tableCollection = book.ParentTable.TableCollection;
			bookCategories = (BookCategoriesTable)tableCollection.GetTable<BookCategory>();
		}

		public override String Name
		{
			get
			{
				if (Model.FranshiseId != 0)
				{
					BookCategory category = bookCategories.GetCategoryByBook(Model);
					if (category != null)
					{
						if (category.HideName != String.Empty)
						{
							return Model.Name.Replace(category.HideName, String.Empty);
						}

						if (category.Name != String.Empty)
                        {
							return Model.Name.Replace(category.Name, String.Empty);
						}
					}
				}
				return Model.Name;
			}
			set { }
		}
	}
}
