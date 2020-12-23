using System;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Platform;
using Avalonia.ReactiveUI;

namespace ControlCatalog
{
    internal class Program
    {
        [STAThread]
        public static int Main(string[] args)
            => BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);

        /// <summary>
        /// This method is needed for IDE previewer infrastructure
        /// </summary>
        public static AppBuilder BuildAvaloniaApp()
        {
            Application AppFactory()
            {
                var app = new Application();
                app.Styles.Add(App.DefaultDark);
                app.RegisterServices();
                var window = new Window();
                var btn = new Button() { Content = "HI" };
                btn.Click += (sender, eventArgs) =>
                {
                    window.Title = "hi";
                };
                window.Content = btn;
                
                window.Show();
                app.Run(window);
                return app;
            }
            return AppBuilder.Configure(AppFactory)
                .LogToTrace()
                .UsePlatformDetect()
                .UseReactiveUI();
        }

        private static void ConfigureAssetAssembly(AppBuilder builder)
        {
            AvaloniaLocator.CurrentMutable
                .GetService<IAssetLoader>()
                .SetDefaultAssembly(typeof(App).Assembly);
        }
    }
}
