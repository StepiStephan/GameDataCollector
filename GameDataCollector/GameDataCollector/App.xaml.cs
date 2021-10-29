using Enums;
using Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GameDataCollector
{
    public partial class App : Application
    {
        private const int GenreCounter = 23;
        
        public static List<Genre> AllGenres { get; set; }

        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            AllGenres = new List<Genre>();
            InitializeComponent();
            var services = new ServiceCollection();
            ServiceProvider = DIKernal.SetDIMicrosoft(services);
            for (int i = 0; i < GenreCounter; i++)
            {
                AllGenres.Add((Genre)i);
            }
            //var workflow = ServiceProvider.GetService<GameDataCollectorWorkflow.Contract.IGameDataWorkflow>();
            //var oldSwitch = workflow.CreateKonsole("Switch", "Alte Switch", 32);
            //var newSwitch = workflow.CreateKonsole("Switch OLED", "Neue Switch", 64);

            //workflow.CreateGame(oldSwitch.Storages.First(), "DOOM 2016", new List<Genre> { Genre.Egoshooter }, 20);
            //workflow.CreateGame(newSwitch.Storages.First(), "Mario Party", new List<Genre> { Genre.Party }, 10);
            //workflow.SaveData();

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
