using Microsoft.Extensions.DependencyInjection;
using System;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Windows;
using WpfApp.Models;
using WpfApp.Services;
using WpfApp.ViewModels;
using WpfApp.Views;

namespace FilmsDBC.Wpf
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

            services.AddSingleton<TablesService>();
            services.AddSingleton<ProfilesService>();
            services.AddSingleton<LanguageService>();
            services.AddSingleton<ScaleService>();
            services.AddSingleton<SettingsService>();

            services.AddTransient<StatusService>();
            services.AddScoped<ExitService>();

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
            LanguageService language = _serviceProvider.GetRequiredService<LanguageService>();
            ScaleService scale = _serviceProvider.GetRequiredService<ScaleService>();

            language.LanguageChanged += LanguageChanged;
            scale.ScaleChanged += ScaleChanged;

            settings.LoadSettings();

            MainWindow.Show();

            base.OnStartup(e);
        }

        private void ScaleChanged(ScaleEnum scale)
        {
            string nameStart = "Resources/Dictionaries/TableControls/Scale.";
            ReplaceSource(nameStart, scale.ToString());
        }

        private void LanguageChanged(CultureInfo culture)
        {
            string nameStart = "Resources/Localizations/lang.";
            ReplaceSource(nameStart, culture.ToString());
        }

        private void ReplaceSource(string resourceNameStart, string resourceNameEnd)
        {
            ResourceDictionary dict = new ResourceDictionary();
            dict.Source = new Uri(resourceNameStart + resourceNameEnd + ".xaml", UriKind.Relative);

            ResourceDictionary oldDict =
                (from d in Resources.MergedDictionaries
                 where d.Source != null && d.Source.OriginalString.StartsWith(resourceNameStart)
                 select d).First();

            if (oldDict != null)
            {
                int ind = Resources.MergedDictionaries.IndexOf(oldDict);
                Resources.MergedDictionaries.Remove(oldDict);
                Resources.MergedDictionaries.Insert(ind, dict);
            }
            else
            {
                Resources.MergedDictionaries.Add(dict);
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
