using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TL_Objects;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    public abstract class ABookElementControl : UserControl, IControls<Book, BookGenre>
    {
		public Book Info { get; protected set; }

		internal void SetSelectedElement()
		{
			MainInfo.MainWindow.BooksMenu.ControlInBuffer = this;
			setVisualSelected();
		}


		public bool SetFindedElement(string search)
		{
			if (this.Info.Name.ToLowerInvariant().Contains(search))
			{
				setVisualFinded();
				return true;
			}

			return false;
		}

		public abstract void SetVisualDefault();
		public abstract void RefreshData();
		public abstract Control ToUpdateControl();
		internal abstract void setVisualSelected();
		internal abstract void setVisualFinded();

		public bool HasSelectedGenre(BookGenre[] selectedGenres)
		{
			foreach (BookGenre genre in selectedGenres)
			{
				if (genre == Info.BookGenre)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasCheckedProperty(bool isWached)
		{
			if (isWached == Info.Readed)
			{
				return true;
			}
			else
			{
				return false;
			}
		}
	}
}
