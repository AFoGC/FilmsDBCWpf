using MobileApp.Models;
using MobileApp.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Presenters
{
    public class FilmsMenuPresenter
    {
        private readonly IFilmsMenuView view;
        private readonly FilmsMenuModel model;

        public FilmsMenuPresenter(IFilmsMenuView view, FilmsMenuModel model)
        {
            this.view = view;
            this.model = model;
        }


    }
}
