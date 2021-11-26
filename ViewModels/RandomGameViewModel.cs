using DataClasses;
using Enums;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;

namespace ViewModels
{
    public class RandomGameViewModel : IRandomGameViewModel
    {
        IGameDataWorkflow workflow;
        public RandomGameViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
        public List<InfoClass> GetGames(IEnumerable<Genre> genres, IEnumerable<Descriptor> descriptors)
        {
            List<Game> games = workflow.Games;

            RemoveGamesWithWrongGenre(genres, games);
            RemoveGamesWithWrongDescription(descriptors, games);
            var infoClasses = ConvertToInfoClass(games);

            return infoClasses;
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

        private void RemoveGamesWithWrongGenre(IEnumerable<Genre> genres, List<Game> games)
        {
            if (genres.Count() != 0)
            {
                foreach (var game in workflow.Games)
                {
                    foreach (var genre in genres)
                    {
                        if (!game.GameGenre.Contains(genre))
                        {
                            if (games.Contains(game))
                            {
                                games.Remove(game);
                            }
                        }
                    }
                }
            }
        }

        private void RemoveGamesWithWrongDescription(IEnumerable<Descriptor> descriptors, List<Game> games)
        {
            if (descriptors.Count() != 0)
            {
                foreach (var game in workflow.Games)
                {
                    foreach (var descriptor in descriptors)
                    {
                        if (!game.GameDiscriptors.Contains(descriptor))
                        {
                            if (games.Contains(game))
                            {
                                games.Remove(game);
                            }
                        }
                    }
                }
            }
        }
    }
}
