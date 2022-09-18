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

        public StatusInfo(StatusEnum @enum, IMainWindowView view)
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
            if (view.Status == Status)
                view.Status = StatusEnum.Normal;
        }
    }
}
