using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Controls;
using TL_Objects;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls
{
	[TypeDescriptionProvider(typeof(AbstractControlDescriptionProvider<AElementControl, UserControl>))]
	public abstract class AElementControl : UserControl, IControls<Film,Genre>
    {
		public Film Info { get; protected set; }

		internal void SetSelectedElement()
		{
			MainInfo.MainWindow.FilmsMenu.ControlInBuffer = this;
			setVisualSelected();
		}

		public bool SetFindedElement(String searchLine)
		{
			if (this.Info.Name.ToLowerInvariant().Contains(searchLine))
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
				if (genre == Info.Genre)
				{
					return true;
				}
			}
			return false;
		}

		public bool HasCheckedProperty(bool isWached)
		{
			if (isWached == Info.Watched)
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
