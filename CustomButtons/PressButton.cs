using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;

namespace CustomButtons
{
    public class PressButton : ButtonBase
    {
        public PressButton() : base()
        {
            this.Click += new RoutedEventHandler(this.this_Click);
        }


        private bool included = false;
        public bool Included
        {
            get { return included; }
            set
            {
                if (included)
                {
                    this.MouseEnter -= new MouseEventHandler(this.this_Included_mouseEnter);
                    this.MouseLeave -= new MouseEventHandler(this.this_Included_mouseLeave);
                    this.MouseDown -= new MouseButtonEventHandler(this.this_Included_mouseDown);
                    this.Click -= new RoutedEventHandler(this.this_Included_mouseUp);
                }
                else
                {
                    this.MouseEnter -= new MouseEventHandler(this.this_NotIncluded_mouseEnter);
                    this.MouseLeave -= new MouseEventHandler(this.this_NotIncluded_mouseLeave);
                    this.MouseDown -= new MouseButtonEventHandler(this.this_NotIncluded_mouseDown);
                    this.Click -= new RoutedEventHandler(this.this_NotIncluded_mouseUp);
                }

                included = value;
                if (included)
                {
                    Background = Included_DefaultColor;
                    this.MouseEnter += new MouseEventHandler(this.this_Included_mouseEnter);
                    this.MouseLeave += new MouseEventHandler(this.this_Included_mouseLeave);
                    this.MouseDown += new MouseButtonEventHandler(this.this_Included_mouseDown);
                    this.Click += new RoutedEventHandler(this.this_Included_mouseUp);
                }
                else
                {
                    Background = DefaultColor;
                    this.MouseEnter += new MouseEventHandler(this.this_NotIncluded_mouseEnter);
                    this.MouseLeave += new MouseEventHandler(this.this_NotIncluded_mouseLeave);
                    this.MouseDown += new MouseButtonEventHandler(this.this_NotIncluded_mouseDown);
                    this.Click += new RoutedEventHandler(this.this_NotIncluded_mouseUp);
                }
            }
        }
        /*
        public static readonly DependencyProperty
         SetTextProperty = DependencyProperty.Register("SetText", typeof(string),
         typeof(PressButton), new PropertyMetadata("",
         new PropertyChangedCallback(OnSetTextChanged)));
        */
        public bool ClickLocked { get; set; }

        public Brush Included_MouseEnterColor { get; set; }
        public Brush Included_DefaultColor { get; set; }
        public Brush Included_MouseDownColor { get; set; }
        public Brush MouseEnterColor { get; set; }
        public Brush DefaultColor { get; set; }
        public Brush MouseDownColor { get; set; }

        private void this_Included_mouseEnter(object sender, EventArgs e)
        {
            this.Background = this.Included_MouseEnterColor;
        }
        private void this_NotIncluded_mouseEnter(object sender, EventArgs e)
        {
            this.Background = this.MouseEnterColor;
        }

        private void this_Included_mouseLeave(object sender, EventArgs e)
        {
            this.Background = this.Included_DefaultColor;
        }
        private void this_NotIncluded_mouseLeave(object sender, EventArgs e)
        {
            this.Background = this.DefaultColor;
        }

        private void this_Included_mouseDown(object sender, EventArgs e)
        {
            this.Background = this.Included_MouseDownColor;
        }
        private void this_NotIncluded_mouseDown(object sender, EventArgs e)
        {
            this.Background = this.MouseDownColor;
        }

        private void this_Included_mouseUp(object sender, EventArgs e)
        {
            if (ClickLocked)
            {
                this.Background = this.MouseEnterColor;
            }
            else
            {
                this.Background = this.Included_MouseEnterColor;
            }
        }
        private void this_NotIncluded_mouseUp(object sender, EventArgs e)
        {
            if (ClickLocked)
            {
                this.Background = this.Included_MouseEnterColor;
            }
            else
            {
                this.Background = this.MouseEnterColor;
            }
        }

        private void this_Click(object sender, RoutedEventArgs e)
        {
            if (!ClickLocked)
            {
                this.Included = !this.Included;

                if (Included)
                {
                    this.Background = this.Included_MouseEnterColor;
                }
                else
                {
                    this.Background = this.MouseEnterColor;
                }
            }
        }
    }
}
