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
        }

        private async void OnClearTappt(object sender, EventArgs e)
        {
            await Task.Factory.StartNew(() => workflow.ClearSelecion());
        }
    }
}
