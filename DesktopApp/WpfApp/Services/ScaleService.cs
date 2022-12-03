using System;

namespace WpfApp.Services
{
    public class ScaleService
    {
        public event Action<ScaleEnum> ScaleChanged;

        public ScaleEnum CurrentScale { get; private set; }

        public ScaleService()
        {
            CurrentScale = ScaleEnum.Medium;
        }

        public void SetScale(ScaleEnum scale)
        {
            CurrentScale = scale;
            ScaleChanged?.Invoke(scale);
        }

        public void SetScale(int scaleCode)
        {
            SetScale((ScaleEnum)scaleCode);
        }
    }
}
