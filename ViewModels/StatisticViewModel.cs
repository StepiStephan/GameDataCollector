using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ViewModels.Contract;
using ViewModels.Contract.DataClasses;

namespace ViewModels
{
    public class StatisticViewModel : IStatisticViewModel
    {
        public List<InfoClass> Data => GetData();

        public string SelectedElementName => selectedName;

        private string selectedName = string.Empty;

        private readonly IGameDataWorkflow workflow;

        public StatisticViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }


        private List<InfoClass> GetData()
        {
            List<InfoClass> result = new List<InfoClass>();
            if (workflow.SelectedStorage != null)
            {
                GetGamesOnStorage(result);
            }
            else 
            {
                if(workflow.SelectedKonsole == null)
                {
                    selectedName = "Total";
                }
                else
                {
                    var konsole = workflow.GetKonsole(workflow.SelectedKonsole);
                    selectedName = $"Konsole {konsole.ConsoleName}";
                }
                GetGamesOnKonsole(result);
            }

            return result;
        }

        private void GetGamesOnKonsole(List<InfoClass> result)
        {
            var storages = workflow.GetStorages();
            var games = workflow.GetGames();
            float gamespace = 0;
            float storageSpace = 0;

            foreach (var storage in storages)
            {
                storageSpace += storage.Space;
            }

            foreach (var game in games)
            {
                var storage = storages.Where(x => x.Games.Contains(game.Id)).First();
                var konsole = workflow.Konsolen.Where(x => x.Storages.Contains(storage.Id)).First();
                result.Add(new InfoClass(game.Name, game.SpaceOnSorage, konsole.Name + "  -> " + storage.Name ));
                gamespace += game.SpaceOnSorage;
            }

            result.Add(new InfoClass("Freier Speicher", storageSpace - gamespace, ""));
        }

        private void GetGamesOnStorage(List<InfoClass> result)
        {
            var storage = workflow.GetStorage(workflow.SelectedStorage);
            var games = workflow.GetGames();
            float gamespace = 0;

            selectedName = $"Speicher {storage.Name}";
            foreach(var game in games)
            {
                var konsole = workflow.Konsolen.Where(x => x.Storages.Contains(storage.Id)).First();
                result.Add(new InfoClass(game.Name, game.SpaceOnSorage, konsole.Name + " -> " + storage.Name));
                gamespace += game.SpaceOnSorage;
            }

            result.Add(new InfoClass("Freier Speicher", storage.Space - gamespace, ""));
        }
    }
}
