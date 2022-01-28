using WpfApp.Visual.MainWindow.GlobalElements.Menus.ACommonElements.ControlsInterface;
using WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.FilmsControls;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TL_Objects;

namespace WpfApp.Visual.MainWindow.GlobalElements.Menus.FilmsMenu.UpdateControls
{
    /// <summary>
    /// Логика взаимодействия для CategoryUpdateControl.xaml
    /// </summary>
    public partial class CategoryUpdateControl : UserControl, IUpdateControl
    {
        private CategoryControl categoryControl = null;
        private Category category = null;

        public CategoryUpdateControl(CategoryControl categoryControl)
        {
            InitializeComponent();
            this.categoryControl = categoryControl;
            this.category = categoryControl.Info;
            refresh();
        }

        public void UpdateElement()
        {
            category.Name = this.name.Text;
            categoryControl.RefreshData();
        }

        private void refresh()
        {
            this.id.Text = category.ID.ToString();
            this.name.Text = category.Name;
        }

        private void btn_AddSelected_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer.GetType() == typeof(SimpleControl))
                {
                    SimpleControl simpleControl = (SimpleControl)MainInfo.MainWindow.FilmsMenu.ControlInBuffer;
                    Film film = simpleControl.Info;
                    if (film.FranshiseId == 0)
                    {
                        film.FranshiseId = category.ID;
                        category.Films.Add(film);
                        categoryControl.AddSimpleCotrol(film);
                        MainInfo.MainWindow.FilmsMenu.controlsPanel.Children.Remove(simpleControl);
                        MainInfo.MainWindow.FilmsMenu.ControlInBuffer = null;
                    }
                }
            }
        }

        private void btn_RemoveSec_Click(object sender, RoutedEventArgs e)
        {
            if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer != null)
            {
                if (MainInfo.MainWindow.FilmsMenu.ControlInBuffer.GetType() == typeof(SimpleControl))
                {
                    SimpleControl simpleControl = (SimpleControl)MainInfo.MainWindow.FilmsMenu.ControlInBuffer;
                    Film film = simpleControl.Info;

                    if (categoryControl.RemoveFilmFromCategory(simpleControl))
                    {

                        if (MainInfo.MainWindow.FilmsMenu.ControlsCondition == FilmsMenuControl.MenuCondition.Category)
                        {
                            int i = 0;
                            foreach (UserControl userControl in MainInfo.MainWindow.FilmsMenu.controlsPanel.Children)
                            {
                                if (userControl.GetType() == typeof(SimpleControl))
                                {
                                    SimpleControl sControl = (SimpleControl)userControl;
                                    if (sControl.Info.ID > film.ID)
                                    {
                                        MainInfo.MainWindow.FilmsMenu.controlsPanel.Children.Insert(i, simpleControl);
                                        return;
                                    }
                                }
                                ++i;
                            }
                            MainInfo.MainWindow.FilmsMenu.controlsPanel.Children.Add(simpleControl);
                        }
                    }
                }
            }
        }
    }
}
