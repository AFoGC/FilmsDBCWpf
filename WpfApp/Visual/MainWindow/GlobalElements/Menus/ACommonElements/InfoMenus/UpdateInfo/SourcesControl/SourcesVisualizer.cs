using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects.CellDataClasses;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo.SourcesControl
{
	public class SourcesVisualizer
	{
		private SourcesControl sourcesControl;
		public SourcesControl SourcesControl
		{
			get { return sourcesControl; }
		}
		private bool isOpen = false;
		public bool IsOpen
		{
			get { return isOpen; }
		}
		private UpdateFormVisualizer updateVisualizer = null;
		public SourcesVisualizer(UpdateFormVisualizer updateVisualizer)
		{
			this.updateVisualizer = updateVisualizer;
			sourcesControl = new SourcesControl(updateVisualizer);
		}

		private void saveSources(object sender, EventArgs e)
		{
			throw new NotImplementedException();
		}

		public void OpenSourceControl(TLCollection<Source> sources)
		{
			sourcesControl.Reinitialize(sources);
			updateVisualizer.ParentControl.Children.Remove(sourcesControl);
			updateVisualizer.ParentControl.Children.Add(sourcesControl);
			if (isOpen == false)
			{
				//System.Drawing.Size size = updateVisualizer.ParentControl.Size;
				//size.Width += 420;
				//updateVisualizer.ParentControl.Size = size;
			}
			isOpen = true;
		}

		public void HideSourceControl()
		{
			updateVisualizer.ParentControl.Children.Remove(sourcesControl);
			if (isOpen == true)
			{
				//System.Drawing.Size size = updateVisualizer.ParentControl.Size;
				//size.Width -= 420;
				//updateVisualizer.ParentControl.Size = size;
			}
			isOpen = false;
		}
	}
}
