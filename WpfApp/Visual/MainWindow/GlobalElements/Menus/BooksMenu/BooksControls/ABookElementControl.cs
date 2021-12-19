using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TL_Objects;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.BooksMenu.BooksControls
{
    public abstract class ABookElementControl : UserControl, IBooksControls
    {
		internal Book bookInfo = null;
		public Book BookInfo
		{
			get { return bookInfo; }
		}

		internal void SetSelectedElement(ABookElementControl controlInBuffer)
		{
			if (controlInBuffer != null)
			{
				controlInBuffer.SetVisualDefault();
			}

			MainInfo.MainWindow.BooksMenu.ControlInBuffer = this;
			setVisualSelected();
		}


		public bool SetFindedElement(string search)
		{
			if (this.BookInfo.Name.ToLowerInvariant().Contains(search))
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
				if (genre == bookInfo.BookGenre)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasReadedProperty(bool isWached)
		{
			if (isWached == BookInfo.Readed)
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
