using GameDataCollector.Views;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector
{
    public partial class AppShell :Shell 
    {
        IGameDataWorkflow workflow;
        public AppShell()
        {
            InitializeComponent();
            workflow = App.ServiceProvider.GetService<IGameDataWorkflow>();
            var fbWorkFlow = App.ServiceProvider.GetService<IFireBaseWorkFlow>();
            workflow.SetIFirebaseWorkflow(fbWorkFlow);
        }

        private async void OnClearTappt(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => workflow.ClearSelecion());
            await DisplayAlert("Auswahl Geleert", "Die ausgewählte Konsole und der ausgewählte Speicer wurde geleert", "OK");
        }
    }
}
