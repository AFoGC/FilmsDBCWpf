using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Presenters
{
    public class MainPresenter
    {
        private readonly MainModel model;
        private readonly IMainPage view;
        public MainPresenter(MainModel model, IMainPage view)
        {
            this.model = model;
            this.view = view;
        }
    }
}
