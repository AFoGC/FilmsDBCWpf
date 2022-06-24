﻿using MobileApp.Models;
using MobileApp.Presenters;
using System;
using System.Collections;
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
    public partial class FilmsPage : ContentPage, IFilmsMenuView
    {

        private readonly FilmsMenuPresenter presenter;
        public FilmsPage(MainModel mainModel)
        {
            InitializeComponent();
            presenter = new FilmsMenuPresenter(this, new FilmsMenuModel(), mainModel);
        }

        public IList<View> MenuControls => stack.Children;

        private void Button_Clicked(object sender, EventArgs e)
        {
            presenter.AddFilm();
        }
    }
}