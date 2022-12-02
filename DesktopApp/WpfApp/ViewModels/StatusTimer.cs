using System;
using System.Windows.Threading;
using WpfApp.ViewModels.Interfaces;

namespace WpfApp.ViewModels
{
    public enum StatusEnum
    {
        Normal,
        Saved,
        UnSaved
    }

    public class StatusTimer : DispatcherTimer
    {
        public StatusEnum Status { get; private set; }
        public IMainViewModel VM { get; private set; }
        public StatusTimer(StatusEnum status, IMainViewModel vm)
        {
            VM = vm;
            Status = status;
            setTimer();
        }

        private void setTimer()
        {
            this.Interval = TimeSpan.FromSeconds(2);
            this.Tick += MessageEnd;
            this.Start();
        }

        private void MessageEnd(object sender, EventArgs e)
        {
            this.Stop();
            if (VM.Status == Status)
                VM.Status = StatusEnum.Normal;
        }
    }
}
