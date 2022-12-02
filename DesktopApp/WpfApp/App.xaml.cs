using Microsoft.Extensions.DependencyInjection;
using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IServiceProvider _serviceProvider;

        public App()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<TablesFileService>();
            services.AddSingleton<ProfilesService>();
            services.AddSingleton<LanguageService>();
            services.AddSingleton<ScaleService>();
            services.AddSingleton<SettingsService>();

            services.AddTransient<StatusService>();

            services.AddTransient<FilmsModel>();
            services.AddTransient<BooksModel>();
            services.AddTransient<SettingsModel>();
            services.AddTransient<ProfilesModel>();
            services.AddTransient<MainWindowModel>();

            services.AddTransient<SettingsViewModel>();
            services.AddTransient<FilmsViewModel>();
            services.AddTransient<BooksViewModel>();
            services.AddTransient<MainViewModel>();

            services.AddSingleton<MainWindowView>(s => new MainWindowView()
            {
                DataContext = s.GetRequiredService<MainViewModel>()
            });

            _serviceProvider = services.BuildServiceProvider();

            InitializeComponent();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            MainWindow = _serviceProvider.GetRequiredService<MainWindowView>();

            SettingsService settings = _serviceProvider.GetRequiredService<SettingsService>();

            settings.ScaleService.ScaleChanged += ScaleChanged;
            settings.LanguageService.LanguageChanged += LanguageChanged;

            settings.LoadSettings();

            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ScaleChanged(ScaleEnum value)
        {
            if (Enum.IsDefined(typeof(ScaleEnum), value))
            {
                ResourceDictionary dict = new ResourceDictionary();
                dict.Source = new Uri(String.Format("Resources/Dictionaries/TableControls/Scale.{0}.xaml", value), UriKind.Relative);

                ResourceDictionary oldDict =
                    (from d in Application.Current.Resources.MergedDictionaries
                     where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Dictionaries/TableControls/Scale.")
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
            }
        }

        private void LanguageChanged(CultureInfo value)
        {
            //1. Change Application Language:
            Thread.CurrentThread.CurrentUICulture = value;

            //2. Creating ResourceDictionary for new culture
            ResourceDictionary dict = new ResourceDictionary();
            if (value.Name != "en")
            {
                dict.Source = new Uri(String.Format("Resources/Localizations/lang.{0}.xaml", value.Name), UriKind.Relative);
            }
            else
            {
                dict.Source = new Uri("Resources/Localizations/lang.xaml", UriKind.Relative);
            }

            //3. Find old ResourceDictionary delete it and add new ResourceDictionary
            ResourceDictionary oldDict = (from d in Application.Current.Resources.MergedDictionaries
                                          where d.Source != null && d.Source.OriginalString.StartsWith("Resources/Localizations/lang.")
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
        }

        private void Application_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show(
                e.Exception.ToString(), 
                "Critical Error", 
                MessageBoxButton.OK, 
                MessageBoxImage.Error
                );
        }
    }
}
