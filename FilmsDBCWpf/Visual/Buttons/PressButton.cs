using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FilmsDBCWpf.Visual.Buttons
{
    public class PressButton : Button
    {
        
        public PressButton() : base()
        {
            this.Click += new RoutedEventHandler(this.this_Click);
            this.Style = new Style();


        }

        
        private Style setDefaultStyle()
        {
            
            Style style = new Style();

            Setter defSetter = new Setter();
            defSetter.Property = Control.BackgroundProperty;
            defSetter.Value = DefaultColor;


            ControlTemplate controlTemplate = new ControlTemplate();
            controlTemplate.TargetType = this.GetType();
            //controlTemplate ;

            Setter tempSetter = new Setter();
            tempSetter.Property = Control.TemplateProperty;
            tempSetter.Value = controlTemplate; //Додеалть это 


            Setter moSetter = new Setter();
            moSetter.Property = Control.BackgroundProperty;
            moSetter.Value = MouseEnterColor;

            Trigger moTrigger = new Trigger();
            moTrigger.Property = UIElement.IsMouseOverProperty;
            moTrigger.Value = true;
            moTrigger.Setters.Add(moSetter);


            Setter mcwSetter = new Setter();
            mcwSetter.Property = Control.BackgroundProperty;
            mcwSetter.Value = mouseDownColor;

            Trigger mcwTrigger = new Trigger();
            mcwTrigger.Property = UIElement.IsMouseCaptureWithinProperty;
            mcwTrigger.Value = true;
            mcwTrigger.Setters.Add(mcwSetter);

            style.Setters.Add(defSetter);
            style.Triggers.Add(moTrigger);
            style.Triggers.Add(mcwTrigger);

            return style;
        }

        private Style setIncludedStyle()
        {
            Style style = new Style();

            Setter defSetter = new Setter();
            defSetter.Property = Control.BackgroundProperty;
            defSetter.Value = Included_DefaultColor;


            ControlTemplate controlTemplate = new ControlTemplate();
            controlTemplate.TargetType = this.GetType();
            //controlTemplate ;

            Setter tempSetter = new Setter();
            tempSetter.Property = Control.TemplateProperty;
            tempSetter.Value = controlTemplate; //Додеалть это 


            Setter moSetter = new Setter();
            moSetter.Property = Control.BackgroundProperty;
            moSetter.Value = Included_MouseEnterColor;

            Trigger moTrigger = new Trigger();
            moTrigger.Property = UIElement.IsMouseOverProperty;
            moTrigger.Value = true;
            moTrigger.Setters.Add(moSetter);


            Setter mcwSetter = new Setter();
            mcwSetter.Property = Control.BackgroundProperty;
            mcwSetter.Value = Included_MouseDownColor;

            Trigger mcwTrigger = new Trigger();
            mcwTrigger.Property = UIElement.IsMouseCaptureWithinProperty;
            mcwTrigger.Value = true;
            mcwTrigger.Setters.Add(mcwSetter);

            style.Setters.Add(defSetter);
            style.Triggers.Add(moTrigger);
            style.Triggers.Add(mcwTrigger);

            return style;
        }


        private bool included = false;
        public bool Included
        {
            get { return included; }
            set
            {
                included = value;
                if (included)
                {
                    this.Style = includedStyle;
                }
                else
                {
                    this.Style = defaultStyle;
                }
            }
        }

        private bool clickLocked = false;
        public bool ClickLocked
        {
            get { return clickLocked; }
            set
            {
                if (!clickLocked)
                {
                    this.Click -= new RoutedEventHandler(this.this_Click);
                }

                clickLocked = value;

                if (clickLocked)
                {
                    this.Click -= new RoutedEventHandler(this.this_Click);
                }
                else
                {
                    this.Click += new RoutedEventHandler(this.this_Click);
                }
            }
        }

        private Brush included_mouseEnterColor;
        private Brush included_defaultColor;
        private Brush included_mouseDownColor;
        private Brush mouseEnterColor;
        private Brush defaultColor;
        private Brush mouseDownColor;

        private Style includedStyle;
        private Style defaultStyle;

        public Brush Included_MouseEnterColor
        {
            get { return included_mouseEnterColor; }
            set
            {
                included_mouseEnterColor = value;
                //includedStyle = setIncludedStyle();
            }
        }
        public Brush Included_DefaultColor
        {
            get { return included_defaultColor; }
            set
            {
                included_defaultColor = value;
                //includedStyle = setIncludedStyle();
            }
        }
        public Brush Included_MouseDownColor
        {
            get { return included_mouseDownColor; }
            set
            {
                included_mouseDownColor = value;
                //includedStyle = setIncludedStyle();
            }
        }

        public Brush MouseEnterColor
        {
            get { return mouseEnterColor; }
            set
            {
                mouseEnterColor = value;
                //defaultStyle = setDefaultStyle();
            }
        }
        public Brush DefaultColor
        {
            get { return defaultColor; }
            set
            {
                defaultColor = value;
                //Background = value;
                //defaultStyle = setDefaultStyle();
            }
        }
        public Brush MouseDownColor
        {
            get { return mouseDownColor; }
            set
            {
                mouseDownColor = value;
                //defaultStyle = setDefaultStyle();
            }
        }

        private void this_Click(object sender, RoutedEventArgs e)
        {
            this.Included = !this.Included;

            if (Included)
            {
                this.Background = this.included_mouseEnterColor;
            }
            else
            {
                this.Background = this.mouseEnterColor;
            }
        }
        
    }
}
