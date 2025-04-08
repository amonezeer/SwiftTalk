using System;
using System.Windows;
using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using SwiftTalk.Data;
using Microsoft.EntityFrameworkCore;
using System.Windows.Media;

namespace SwiftTalk
{
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            var services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();

            ChangeTheme("Light");
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer("Server=Amonezeer;Database=AuthAppDB_New;Integrated Security=True;TrustServerCertificate=True;"));

            services.AddScoped<DatabaseService>();
            services.AddScoped<EmailService>();
            services.AddScoped<MainWindow>();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }

        public void ChangeTheme(string baseTheme)
        {
            Resources.MergedDictionaries.Clear();

            var baseThemeMode = baseTheme == "Dark" ? BaseTheme.Dark : BaseTheme.Light;

            var primaryColor = (Color)ColorConverter.ConvertFromString("#673AB7"); 
            var secondaryColor = (Color)ColorConverter.ConvertFromString("#9C27B0"); 

            var theme = Theme.Create(
                baseThemeMode,
                primaryColor,
                secondaryColor
            );

            var themeDict = new ResourceDictionary
            {
                Source = new Uri($"pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesignTheme.{baseTheme}.xaml")
            };

            var materialDict = new ResourceDictionary
            {
                Source = new Uri("pack://application:,,,/MaterialDesignThemes.Wpf;component/Themes/MaterialDesign3.Defaults.xaml")
            };

            Resources.MergedDictionaries.Add(themeDict);
            Resources.MergedDictionaries.Add(materialDict);
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Primary/MaterialDesignColor.DeepPurple.xaml") });
            Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri("pack://application:,,,/MaterialDesignColors;component/Themes/Recommended/Secondary/MaterialDesignColor.Purple.xaml") });

            var paletteHelper = new PaletteHelper();
            paletteHelper.SetTheme(theme);
        }
    }
}