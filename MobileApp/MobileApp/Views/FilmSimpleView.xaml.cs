using MobileApp.ModelBinder;
using MobileApp.Presenters;
using MobileApp.ViewInterfaces;
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
    public partial class FilmSimpleView : ContentView, IView
    {
        public FilmPresenter Presenter { get; private set; }
        double IView.Height { get => this.Height; }

        public FilmSimpleView(Film film)
        {
            InitializeComponent();
            Presenter = new FilmPresenter(film, this);
            BindingContext = new FilmBinder(film);
        }

        public void SetVisualDefault()
        {
            throw new NotImplementedException();
        }

        public void SetVisualSelected()
        {
            throw new NotImplementedException();
        }

        public void SetVisualFinded()
        {
            throw new NotImplementedException();
        }

        public void SelfRemove()
        {
            throw new NotImplementedException();
        }
    }
}