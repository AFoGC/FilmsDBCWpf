using BL_Films;
using BO_Films;
using ProfilesConfig;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using TablesLibrary.Interpreter;
using TablesLibrary.Interpreter.Table;
using TablesLibrary.Interpreter.TableCell;
using TL_Objects;
using TL_Tables;

namespace WpfApp.Models
{
    public class MainWindowModel : IUserModel
    {
        public bool InfoUnsaved { get; set; }
        public TableCollection TableCollection { get; private set; }
        public ProgramSettings Settings { get; private set; }
        public TLTables Tables { get; private set; }

        public event EventHandler UserChanged;
        private UserBO userBO;

        public bool IsLoggedIn
        {
            get
            {
                return LoggedInUser != null;
            }
        }
        public UserBO LoggedInUser
        {
            get { return userBO; }
            set
            {
                userBO = value;
                EventHandler handler = UserChanged;
                if (null != handler) handler(userBO, EventArgs.Empty);
            }
        }

        public static CultureInfo Language
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentUICulture;
            }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                if (value == System.Threading.Thread.CurrentThread.CurrentUICulture) return;

                //1. Меняем язык приложения:
                System.Threading.Thread.CurrentThread.CurrentUICulture = value;

                //2. Создаём ResourceDictionary для новой культуры
                ResourceDictionary dict = new ResourceDictionary();
                switch (value.Name)
                {
                    case "ru":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    case "uk-UA":
                        dict.Source = new Uri(String.Format("Resources/lang.{0}.xaml", value.Name), UriKind.Relative);
                        break;
                    default:
                        dict.Source = new Uri("Resources/lang.xaml", UriKind.Relative);
                        break;
                }

                //3. Находим старую ResourceDictionary и удаляем его и добавляем новую ResourceDictionary
                ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                              where d.Source != null && d.Source.OriginalString.StartsWith("Resources/lang.")
                                              select d).First();
                if (oldDict != null)
                {
                    int ind = Application.Current.Resources.MergedDictionaries.IndexOf(oldDict);
                    Application.Current.Resources.MergedDictionaries.Remove(oldDict);
                    Application.Current.Resources.MergedDictionaries.Insert(ind, dict);
                }
                else
                {
                    Application.Current.Resources.MergedDictionaries.Add(dict);
                }

                //4. Вызываем евент для оповещения всех окон.
            }
        }

        public MainWindowModel()
        {
            InfoUnsaved = false;
            Language = new CultureInfo("uk-UA");
            TableCollection = new TableCollection();
            Tables = new TLTables(TableCollection);
            TableCollection.TableSave += boolSaved;
            TableCollection.CellInTablesChanged += TableCollection_CellInTablesChanged;

            string localPath = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);

            Settings = ProgramSettings.Initialize(TableCollection, localPath, Path.Combine(localPath, "ProgramSetting.xml"));
            if (Settings.StartUser.LoggedIn)
            {
                LoggedInUser = UserBL.LogIn(Settings.StartUser.Email, Settings.StartUser.Email);
            }

            TableCollection.FileEncoding = Encoding.UTF8;
            InfoUnsaved = false;
        }

        private void TableCollection_CellInTablesChanged(object sender, EventArgs e)
        {
            InfoUnsaved = true;
        }

        private void boolSaved(object sender, EventArgs e)
        {
            InfoUnsaved = false;
        }
    }
}
