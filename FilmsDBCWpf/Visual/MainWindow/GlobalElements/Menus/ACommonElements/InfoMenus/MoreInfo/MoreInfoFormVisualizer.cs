using FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.UpdateInfo;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Controls;

namespace FilmsDBCWpf.Visual.MainWindow.GlobalElements.Menus.ACommonElements.InfoMenus.MoreInfo
{
	public class MoreInfoFormVisualizer
	{

		private MoreInfoControl infoControl = new MoreInfoControl();
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
		}
		
		public void OpenMoreInfoForm(ISimpleControl simpleControl, UpdateFormVisualizer updateVisualizer)
		{
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
