using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TL_Objects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilmsPage : ContentPage
    {
        public FilmsPage()
        {
            InitializeComponent();
            stack.Children.Add(new FilmSimpleView(new Film()));
            stack.Children.Add(new FilmSimpleView(new Film()));
            stack.Children.Add(new FilmSimpleView(new Film()));
        }
    }
}