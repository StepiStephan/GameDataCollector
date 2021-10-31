using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using DevExpress.XamarinForms.Charts;
using System.Collections.ObjectModel;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatisticPage : ContentPage
    {
        public IStatisticViewModel viewModel;
        public List<InfoClass> Data { get => viewModel.Data; }
        public StatisticPage()
        {
            viewModel = App.ServiceProvider.GetService<IStatisticViewModel>();
            InitializeComponent();
            Title = "Statistic";
            pieAdapter.DataSource = Data;
            Appearing += StatisticPage_Appearing;

        }
    

        private void StatisticPage_Appearing(object sender, EventArgs e)
        {
            GetData();
        }

        private void GetData()
        {



        }
    }
}