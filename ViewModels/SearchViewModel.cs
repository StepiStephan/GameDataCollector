using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;

namespace ViewModels
{
    public class SearchViewModel : ISearchViewModel
    {
        private IGameDataWorkflow workflow;

        public SearchViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }

        public IEnumerable<InfoClass> GetMatchingGame(string newTextValue)
        {
            List<InfoClass> result = new List<InfoClass>();

            var games = workflow.Games.Where(x => x.Name.Contains(newTextValue));
            result = ConvertToInfoClass(games);

            return result;
        }

        private List<InfoClass> ConvertToInfoClass(IEnumerable<Game> games)
        {
            List<InfoClass> result = new List<InfoClass>();
            foreach (var game in games)
            {
                var storage = workflow.Storages.Where(x => x.Games.Contains(game.Id)).First();
                var konsole = workflow.Konsolen.Where(x => x.Storages.Contains(storage.Id)).First();
                result.Add(new InfoClass(game.Name, game.SpaceOnSorage, $"{storage.Name} -> {konsole.Name}"));
            }
            return result;
        }
    }
}
