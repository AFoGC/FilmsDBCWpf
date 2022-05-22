
using MobileApp.Models;
using MobileApp.Presenters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : TabbedPage, IMainPage
    {
        private readonly MainPresenter presenter;
        public MainPage()
        {
            InitializeComponent();

            MainModel model = new MainModel();
            presenter = new MainPresenter(model, this);
            this.Children.Add(new FilmsPage(model) { Title = "Films" });
            this.Children.Add(new ContentPage { Title = "Tab 2" });
            this.Children.Add(new ContentPage { Title = "Tab 3" });
        }
    }
}