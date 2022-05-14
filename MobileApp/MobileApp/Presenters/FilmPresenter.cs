using MobileApp.PresenterInterfaces;
using MobileApp.ViewInterfaces;
using System;
using System.Collections.Generic;
using System.Text;
using TablesLibrary.Interpreter;
using TL_Objects;

namespace MobileApp.Presenters
{
    public class FilmPresenter : BasePresenter<Film>
    {
        public FilmPresenter(Film film, IView view) : base(film, view)
        {

        }

        public override bool HasCheckedProperty(bool isReaded)
        {
            throw new NotImplementedException();
        }

        public override bool SetFindedElement(string search)
        {
            throw new NotImplementedException();
        }

        public override void SetSelectedElement()
        {
            throw new NotImplementedException();
        }

        public override void SetVisualDefault()
        {
            throw new NotImplementedException();
        }
    }
}
