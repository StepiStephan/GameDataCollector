using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GameDataCollector.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchPage : ContentPage
    {
        private ISearchViewModel viewModel;
        public ObservableCollection<InfoClass> Games { get; }
        public SearchPage()
        {
            viewModel = App.ServiceProvider.GetService<ISearchViewModel>();
            Games = new ObservableCollection<InfoClass>(viewModel.GetMatchingGame(""));
            InitializeComponent();
            result.ItemsSource = Games;

            gameName.TextChanged += GameName_TextChanged;
        }

        private void GameName_TextChanged(object sender, TextChangedEventArgs e)
        {
            Games.Clear();
            var games = viewModel.GetMatchingGame(e.NewTextValue);

            foreach(var game in games)
            {
                Games.Add(game);
            }
        }
    }
}