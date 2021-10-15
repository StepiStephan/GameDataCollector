using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using Xamarin.Forms;

namespace GameDataCollector
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            ServiceProvider = DIKernal.SetDIMicrosoft(services);
            
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
