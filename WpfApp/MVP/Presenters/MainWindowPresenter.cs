using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using WpfApp.MVP.Models;
using WpfApp.Visual.HelpWindows.ExitWindow;

namespace WpfApp.MVP.Presenters
{
    public class MainWindowPresenter
    {
        private readonly MainWindowModel model;

        public MainWindowPresenter(MainWindowModel model)
        {
            this.model = model;
        }

        public void WindowLoaded()
        {
            MainInfo.TableCollection.LoadTables();
        }

        public void WindowKeyDown(KeyEventArgs e)
        {
            if (e.Key == Key.S && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                MainInfo.TableCollection.SaveTables();
            }
        }

        public void WindowClosed()
        {
            if (MainInfo.IsLoggedIn)
            {
                MainInfo.Settings.Profiles.SendProfilesToDB(MainInfo.LoggedInUser);
            }
        }

        public bool Unsaved { get { return model.InfoUnsaved; } }
        public void Save(bool save)
        {
            if (save)
            {
                MainInfo.TableCollection.SaveTables();
            }
        }

        /*
        if (presenter.Unsaved)
        {
            ExitWindow exitForm = new ExitWindow();
            exitForm.ShowDialog();

            presenter.Save(exitForm.Save)

            e.Cancel = !exitForm.CloseProg;
        }
        */

        /*
        public void WindowClosing(CancelEventArgs e)
        {
            if (model.InfoUnsaved)
            {
                ExitWindow exitForm = new ExitWindow();
                exitForm.ShowDialog();

                if (exitForm.Save == true)
                {
                    MainInfo.TableCollection.SaveTables();
                }

                e.Cancel = !exitForm.CloseProg;
            }
        }
        */
    }
}
