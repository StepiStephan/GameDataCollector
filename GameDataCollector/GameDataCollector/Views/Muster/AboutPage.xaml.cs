﻿using GameDataCollectorWorkflow.Contract;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var workflow = App.ServiceProvider.GetService<IGameDataWorkflow>();
        }
    }
} 