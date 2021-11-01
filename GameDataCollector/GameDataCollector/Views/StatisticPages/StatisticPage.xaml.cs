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
        private const string TitleString = "Statistic";
        public ObservableCollection<InfoClass> Data { get; }
        public StatisticPage()
        {
            viewModel = App.ServiceProvider.GetService<IStatisticViewModel>();
            Data = new ObservableCollection<InfoClass>(viewModel.Data);
            InitializeComponent();
            Title = TitleString;
            pieAdapter.DataSource = Data;
            Appearing += StatisticPage_Appearing;
            chart.SelectionChanged += Chart_SelectionChanged;
        }

        private async void Chart_SelectionChanged(object sender, DevExpress.XamarinForms.Charts.SelectionChangedEventArgs e)
        {
            var data = (InfoClass)((DataSourceKey)e.SelectedObjects.First()).DataObject;
            await DisplayAlert(data.Name, $"Größe: {data.Space}" + Environment.NewLine + $"Speicherort: {data.SavePoint}", "OK");
        }

        private void StatisticPage_Appearing(object sender, EventArgs e)
        {
            Data.Clear();
            var infos = viewModel.Data;
            Title = viewModel.SelectedElementName + " " + TitleString;
            foreach(var info in infos)
            {
                Data.Add(info);
            }
        }


    }
}