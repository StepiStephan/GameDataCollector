using GameDataCollector.Services;
using GameDataCollector.Views;
using GameDataCollectorWorkflow;
using GameDataCollectorWorkflow.Contract;
using Infrastructure;
using Ninject;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DIKernal  dIKernal= new DIKernal();
            dIKernal.SetXamarinDI();


            var workflow = DependencyService.Get<IGameDataWorkflow>();
            DependencyService.Register<MockDataStore>();
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
