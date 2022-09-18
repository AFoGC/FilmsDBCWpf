using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace FilmsUCWpf
{
    public static class BrushColors
    {
        public static SolidColorBrush SelectColor { get; private set; }
        public static SolidColorBrush DefaultColor { get; private set; }
        public static SolidColorBrush FindColor { get; private set; }
        public static SolidColorBrush CategorySelectColor { get; private set; }

        static BrushColors()
        {
            SelectColor = new SolidColorBrush(Color.FromRgb(0, 176, 72));
            DefaultColor = new SolidColorBrush(Color.FromRgb(53, 53, 53));
            FindColor = new SolidColorBrush(Color.FromRgb(0, 116, 229));
        }
    }
}
