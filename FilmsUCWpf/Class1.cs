using FilmsUCWpf.Presenters;
using FilmsUCWpf.Views;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TL_Objects;

namespace FilmsUCWpf
{
	class Class1
	{
		public void a()
		{
			Film film = new Film();
			FilmPresenter presenter = new FilmPresenter(film, null);
			FilmControl control = new FilmControl(presenter);
			
			//StackPanel panel = new StackPanel();
			//IList elements = panel.Children;
			//UIElementCollection collection = new UIElementCollection(); 
			//presenter.View = new FilmControl(presenter);
		}
	}
}
