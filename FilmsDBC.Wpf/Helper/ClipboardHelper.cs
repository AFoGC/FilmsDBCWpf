using System;

namespace FilmsDBC.Wpf.Helper
{
    public static class ClipboardHelper
    {
        private static Action<string> _setText;

        public static Action<string> SetText
        {
            get => _setText;
            set
            {
                if (_setText != null)
                    throw new ArgumentException("Delegate already setted");

                _setText = value;
            }
        }
    }
}
