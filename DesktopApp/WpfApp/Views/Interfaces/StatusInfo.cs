using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Threading;

namespace WpfApp.Views.Interfaces
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
        private readonly IMainWindowView view;

        public StatusEnum Status { get; private set; }
        public String DictionaryString { get; private set; }
        public SolidColorBrush SatusColor { get; private set; }

        private StatusInfo(StatusEnum @enum, IMainWindowView view)
        {
            this.view = view;
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
            if (view.Status.Status == Status)
                view.Status = GetInfo(StatusEnum.Normal, view);
        }

        public static StatusInfo GetInfo(StatusEnum @enum, IMainWindowView view)
        {
            StatusInfo status = new StatusInfo(@enum, view);
            switch (@enum)
            {
                case StatusEnum.Normal:
                    status.DictionaryString = String.Empty;
                    status.SatusColor = new SolidColorBrush(Color.FromRgb(31, 31, 31));
                    break;
                case StatusEnum.Saved:
                    status.DictionaryString = "sb_saved";
                    status.SatusColor = new SolidColorBrush(Color.FromRgb(0, 176, 72));
                    break;
                case StatusEnum.UnSaved:
                    status.DictionaryString = "sb_unsaved";
                    status.SatusColor = new SolidColorBrush(Color.FromRgb(230, 46, 76));
                    break;
                default:
                    return null;
            }
            return status;
        }
    }
}
