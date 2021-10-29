using GameDataCollector.ViewModels;
using GameDataCollector.Views;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        IGameDataWorkflow workflow;
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
            Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
            workflow = App.ServiceProvider.GetService<IGameDataWorkflow>();
        }

        private async void OnMenuItemClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//LoginPage");
        }

        private async void OnClearTappt(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => workflow.ClearSelecion());
        }
    }
}
