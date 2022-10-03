using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp.ViewModels.Interfaces
{
    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }

    public class StatusInfo
    {
        private DispatcherTimer timer;
        private readonly IMainViewModel viewModel;

        public StatusEnum Status { get; private set; }
        public String DictionaryString { get; private set; }
        public String DictionaryColor { get; private set; }

        private StatusInfo(StatusEnum @enum, IMainViewModel viewModel)
        {
            this.viewModel = viewModel;
            Status = @enum;
            if (Status == StatusEnum.Saved)
            {
                setTimer();
            }
        }

        private void setTimer()
        {
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(2);
            timer.Tick += MessageEnd;
            timer.Start();
        }

        private void MessageEnd(object sender, EventArgs e)
        {
            timer.Stop();
            if (viewModel.Status.Status == Status)
                viewModel.Status = GetInfo(StatusEnum.Normal, viewModel);
        }

        public static StatusInfo GetInfo(StatusEnum @enum, IMainViewModel viewModel)
        {
            StatusInfo status = new StatusInfo(@enum, viewModel);
            switch (@enum)
            {
                case StatusEnum.Normal:
                    status.DictionaryString = String.Empty;
                    status.DictionaryColor = "ButtonDarkGray";
                    break;
                case StatusEnum.Saved:
                    status.DictionaryString = "sb_saved";
                    status.DictionaryColor = "ButtonGreen";
                    break;
                case StatusEnum.UnSaved:
                    status.DictionaryString = "sb_unsaved";
                    status.DictionaryColor = "ButtonRed";
                    break;
                default:
                    return null;
            }
            return status;
        }
    }
}
