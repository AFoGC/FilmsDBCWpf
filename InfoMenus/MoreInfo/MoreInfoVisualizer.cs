using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using InfoMenus.Interfaces;
using InfoMenus.UpdateInfo;

namespace InfoMenus.MoreInfo
{
    public class MoreInfoVisualizer
    {
		public UpdateInfoVisualizer UpdateVisualizer { get; set; }
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
		public MoreInfoVisualizer(Canvas parentControl)
		{
			this.parentControl = parentControl;
			this.infoControl = new MoreInfoControl(this);
		}

		public void OpenMoreInfoForm(ISimpleControl simpleControl)
		{
			parentControl.Children.Remove(infoControl);
			infoControl.Reinitialize(simpleControl);
			parentControl.Children.Add(infoControl);



			if (UpdateVisualizer.IsOpen)
			{
				UpdateVisualizer.HideUpdateControl();
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
