using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;
using TL_Objects;

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls
{
    public abstract class AElementControl : UserControl, IFilmsControl
    {
		internal Film filmInfo = null;
		public Film FilmInfo
		{
			get { return filmInfo; }
		}

		internal void SetSelectedElement(AElementControl controlInBuffer)
		{
			if (controlInBuffer != null)
			{
				controlInBuffer.SetVisualDefault();
			}

			MainInfo.MainWindow.FilmsMenu.ControlInBuffer = this;
			setVisualSelected();
		}

		public bool SetFindedElement(String searchLine)
		{
			if (this.FilmInfo.Name.ToLowerInvariant().Contains(searchLine))
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

		public bool HasSelectedGenre(Genre[] selectedGenres)
		{
			foreach (Genre genre in selectedGenres)
			{
				if (genre == filmInfo.Genre)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasWatchedProperty(bool isWached)
		{
			if (isWached == FilmInfo.Watched)
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
