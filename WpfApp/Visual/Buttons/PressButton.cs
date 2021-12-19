using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp.Visual.Buttons
{
    public class PressButton : Button
    {
        
        public PressButton() : base()
        {
            this.Click += new RoutedEventHandler(this.this_Click);
            //Included = false;
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
                    Background = included_defaultColor;
                    this.MouseEnter += new MouseEventHandler(this.this_Included_mouseEnter);
                    this.MouseLeave += new MouseEventHandler(this.this_Included_mouseLeave);
                    this.MouseDown += new MouseButtonEventHandler(this.this_Included_mouseDown);
                    this.Click += new RoutedEventHandler(this.this_Included_mouseUp);
                }
                else
                {
                    Background = defaultColor;
                    this.MouseEnter += new MouseEventHandler(this.this_NotIncluded_mouseEnter);
                    this.MouseLeave += new MouseEventHandler(this.this_NotIncluded_mouseLeave);
                    this.MouseDown += new MouseButtonEventHandler(this.this_NotIncluded_mouseDown);
                    this.Click += new RoutedEventHandler(this.this_NotIncluded_mouseUp);
                }
            }
        }

        private bool clickLocked = false;
        public bool ClickLocked
        {
            get { return clickLocked; }
            set
            {
                clickLocked = value;
            }
        }

        private Brush included_mouseEnterColor;
        private Brush included_defaultColor;
        private Brush included_mouseDownColor;
        private Brush mouseEnterColor;
        private Brush defaultColor;
        private Brush mouseDownColor;

        public Brush Included_MouseEnterColor
        {
            get { return included_mouseEnterColor; }
            set
            {
                included_mouseEnterColor = value;
            }
        }
        public Brush Included_DefaultColor
        {
            get { return included_defaultColor; }
            set
            {
                included_defaultColor = value;
            }
        }
        public Brush Included_MouseDownColor
        {
            get { return included_mouseDownColor; }
            set
            {
                included_mouseDownColor = value;
            }
        }

        public Brush MouseEnterColor
        {
            get { return mouseEnterColor; }
            set
            {
                mouseEnterColor = value;
            }
        }
        public Brush DefaultColor
        {
            get { return defaultColor; }
            set
            {
                defaultColor = value;
            }
        }
        public Brush MouseDownColor
        {
            get { return mouseDownColor; }
            set
            {
                mouseDownColor = value;
            }
        }

        private void this_Included_mouseEnter(object sender, EventArgs e)
        {
            this.Background = this.included_mouseEnterColor;
        }
        private void this_NotIncluded_mouseEnter(object sender, EventArgs e)
        {
            this.Background = this.mouseEnterColor;
        }

        private void this_Included_mouseLeave(object sender, EventArgs e)
        {
            this.Background = this.included_defaultColor;
        }
        private void this_NotIncluded_mouseLeave(object sender, EventArgs e)
        {
            this.Background = this.defaultColor;
        }

        private void this_Included_mouseDown(object sender, EventArgs e)
        {
            this.Background = this.included_mouseDownColor;
        }
        private void this_NotIncluded_mouseDown(object sender, EventArgs e)
        {
            this.Background = this.mouseDownColor;
        }

        private void this_Included_mouseUp(object sender, EventArgs e)
        {
            if (clickLocked)
            {
                this.Background = this.mouseEnterColor;
            }
            else
            {
                this.Background = this.included_mouseEnterColor;
            }
        }
        private void this_NotIncluded_mouseUp(object sender, EventArgs e)
        {
            if (clickLocked)
            {
                this.Background = this.included_mouseEnterColor;
            }
            else
            {
                this.Background = this.mouseEnterColor;
            }
        }

        private void this_Click(object sender, RoutedEventArgs e)
        {
            if (!clickLocked)
            {
                this.Included = !this.Included;

                if (Included)
                {
                    this.Background = this.included_mouseEnterColor;
                    //this.this_Included_mouseEnter(this, e);
                }
                else
                {
                    this.Background = this.mouseEnterColor;
                    //this.this_NotIncluded_mouseEnter(this, e);
                }
            }
        }
        
    }
}
