using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApp.Models;
using WpfApp.Presenters;
using WpfApp.Views.Interfaces;

namespace WpfApp.Views
{
    /// <summary>
    /// Логика взаимодействия для SettingsMenuView.xaml
    /// </summary>
    public partial class SettingsMenuView : UserControl, ISettingsMenuView
    {
        public IList SettingsList => SettingsListPanel.Children;

        private readonly SettingsMenuPresenter presenter;
        public SettingsMenuView(MainWindowModel model)
        {
            presenter = new SettingsMenuPresenter(new SettingsMenuModel(), this, model);
            InitializeComponent();
            LogInUser.SetUserModel(model);

            this.SettingsList.Add(new ProfileContainerView(model.Settings));
            GenreConteinerView conteinerView;

            conteinerView = new GenreConteinerView(model.TableCollection, GenrePresenterEnum.FilmGenre);
            conteinerView.MenuLabel.SetResourceReference(System.Windows.Controls.Label.ContentProperty, "st_fgenre_title");
            this.SettingsList.Add(conteinerView);

            conteinerView = new GenreConteinerView(model.TableCollection, GenrePresenterEnum.BookGenre);
            conteinerView.MenuLabel.SetResourceReference(System.Windows.Controls.Label.ContentProperty, "st_bgenre_title");
            this.SettingsList.Add(conteinerView);

            this.SettingsList.Add(new LanguageSettingsView(model.Settings));
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("Довідка до програми.pdf");
        }

        private void SendToDB_Click(object sender, RoutedEventArgs e)
        {
            presenter.SendToDB();
        }

        private void GetFromDB_Click(object sender, RoutedEventArgs e)
        {
            presenter.GetFromDB();
        }
    }
}
