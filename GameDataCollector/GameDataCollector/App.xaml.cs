﻿using Enums;
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
        private const int DescriptorCounter = 10;
        public static List<Genre> AllGenres { get; set; }
        public static List<Descriptor> AllDescriptors { get; set; }
        public static IServiceProvider ServiceProvider { get; set; }
        public App()
        {
            DevExpress.XamarinForms.DataGrid.Initializer.Init();
            DevExpress.XamarinForms.Charts.Initializer.Init();
            AllGenres = new List<Genre>();
            AllDescriptors = new List<Descriptor>();
            InitializeComponent();
            var services = new ServiceCollection();
            ServiceProvider = DIKernal.SetDIMicrosoft(services);
            for (int i = 0; i < GenreCounter; i++)
            {
                AllGenres.Add((Genre)i);
            }
            for (int i = 0; i < DescriptorCounter; i++)
            {
                AllDescriptors.Add((Descriptor)i);
            }

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
