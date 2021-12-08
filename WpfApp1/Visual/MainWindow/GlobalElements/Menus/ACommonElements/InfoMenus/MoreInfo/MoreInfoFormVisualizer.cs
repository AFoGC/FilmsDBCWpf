using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo
{
	public class MoreInfoFormVisualizer
	{

		private MoreInfoControl infoControl;
		public MoreInfoControl MoreInfoControl
		{
			get { return infoControl; }
		}

		private bool isOpen = false;
		public bool IsOpen
		{
			get { return isOpen; }
		}

		private Canvas parentControl = null;
		public MoreInfoFormVisualizer(Canvas parentControl)
		{
			this.parentControl = parentControl;
			this.infoControl = new MoreInfoControl(this);
		}
		
		public void OpenMoreInfoForm(ISimpleControl simpleControl, UpdateFormVisualizer updateVisualizer)
		{
			parentControl.Children.Remove(infoControl);
			infoControl.Reinitialize(simpleControl);
			parentControl.Children.Add(infoControl);
			
			

			if (updateVisualizer.IsOpen)
			{
				updateVisualizer.HideUpdateControl();
			}

			isOpen = true;

		}

		public void HideMoreInfoControl()
		{
			isOpen = false;
			parentControl.Children.Remove(infoControl);
		}
		
	}
}
