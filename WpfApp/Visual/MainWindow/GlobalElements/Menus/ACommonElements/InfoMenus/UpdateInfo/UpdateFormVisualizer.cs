﻿using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo.SourcesControl;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo
{
    public class UpdateFormVisualizer
    {
		public MoreInfoFormVisualizer MoreVisualizer { get; set; }
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

		public UpdateFormVisualizer(Canvas parentControl)
		{
			this.parentControl = parentControl;
			this.sourcesVisualizer = new SourcesVisualizer(this);
			this.updateControl = new UpdateControl(this);
		}

		public void OpenUpdateControl(IToUpdateControl userControl)
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
