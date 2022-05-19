using MobileApp.Models;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Text;

namespace MobileApp.Presenters
{
    public class FilmsMenuPresenter
    {
        private readonly IFilmsMenuView view;
        private readonly FilmsMenuModel model;
        private readonly MainModel mainModel;

        public FilmsMenuPresenter(IFilmsMenuView view, FilmsMenuModel model, MainModel mainModel)
        {
            this.view = view;
            this.model = model;
            this.mainModel = mainModel;
        }


    }
}
