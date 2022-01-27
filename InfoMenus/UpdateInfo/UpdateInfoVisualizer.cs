using InfoMenus.Interfaces;
using InfoMenus.MoreInfo;
using InfoMenus.UpdateInfo.SourceInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace InfoMenus.UpdateInfo
{
    public class UpdateInfoVisualizer
    {
		public MoreInfoVisualizer MoreVisualizer { get; set; }
		private UpdateControl updateControl;
		public UpdateControl UpdateControl { get { return updateControl; } }
		private bool isOpen = false;
		public bool IsOpen { get { return isOpen; } }

		private SourcesVisualizer sourcesVisualizer;
		public SourcesVisualizer SourcesVisualizer
		{
			get { return sourcesVisualizer; }
		}

		private Canvas parentControl = null;
		public Canvas ParentControl
		{
			get { return parentControl; }
		}

		public UpdateInfoVisualizer(Canvas parentControl)
		{
			this.parentControl = parentControl;
			this.sourcesVisualizer = new SourcesVisualizer(this);
			this.updateControl = new UpdateControl(this);
		}

		public void OpenUpdateControl(IControls userControl)
		{
			updateControl.Reinitialize(userControl);
			parentControl.Children.Remove(updateControl);
			parentControl.Children.Add(updateControl);

			if (MoreVisualizer.IsOpen)
			{
				MoreVisualizer.HideMoreInfoControl();
			}
			if (sourcesVisualizer.IsOpen)
			{
				sourcesVisualizer.HideSourceControl();
			}

			isOpen = true;
		}

		public void HideUpdateControl()
		{
			isOpen = false;
			parentControl.Children.Remove(updateControl);

			if (sourcesVisualizer.IsOpen)
			{
				sourcesVisualizer.HideSourceControl();
			}
		}
	}
}
