using DataClasses;
using Enums;
using GameDataCollectorWorkflow.Contract;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Contract;

namespace ViewModels
{
    public class GamesViewModel : IGameViewModel
    {
        private readonly IGameDataWorkflow workflow;
        public GamesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }

        public List<Game> Games => workflow.Games;

        public Konsole SelectedKonsole { get => workflow.GetKonsole(workflow.SelectedKonsole) ; set => workflow.SelectKonsole(value.Id); }
        public Storage SelectedStorage { get => workflow.GetStorage(workflow.SelectedStorage); set => workflow.SelectStorage(value.Id); }

        public void AddDescriptors(string id, List<Descriptor> selectedDescriptors)
        {
            workflow.AddDescriptors(id, selectedDescriptors);
        }

        public void AddGenre(string gameId, List<Genre> genre)
        {
            workflow.AddGenre(gameId, genre);
        }

        public Game CreateGame(string storageId, string name, List<Genre> genres, float space)
        {
            var game = workflow.CreateGame(storageId, name, genres, space);
            return game;

        }

        public void DeleteGame(string gameId)
        {
            workflow.DeleteGame(gameId);
        }

        public void DeleteGenre(string gameId, Genre genre)
        {
            workflow.DeleteGenre(gameId, genre);
        }

        public void DeleteDescription(string gameId, Descriptor descriptor)
        {
            workflow.DeleteDescriptor(gameId, descriptor);
        }

        public void EditGame(string gameId, string name, float space)
        {
            workflow.EditGame(gameId, name, space);
        }

        public Game GetGame(string gameId)
        {
            return workflow.GetGame(gameId);
        }

        public List<Game> GetGames()
        {
            return workflow.GetGames();
        }

        public string[] GetInfos(Game game)
        {
            var storage = workflow.GetStorages().Where(x => x.Games.Contains(game.Id)).FirstOrDefault();
            if(storage != null)
            { 
                var konsole = workflow.Konsolen.Where(x => x.Storages.Contains(storage.Id)).First();
                return new string[]
                {
                    konsole.Name,
                    storage.Name
                };
            }
            return new string[] {string.Empty, string.Empty};
        }

        public List<Konsole> GetKonsolen()
        {
            return workflow.Konsolen;
        }

        public List<Storage> GetStorages()
        {
            return workflow.GetStorages();
        }

        public void MoveGame(string oldStorageId, string gameId, string newStorageId)
        {
            workflow.MoveGame(oldStorageId, gameId, newStorageId);
        }

        public void CopyGame(string oldStorageId, string gameId, string newStorageId)
        {
            workflow.CopyGame(oldStorageId, gameId, newStorageId);
        }
    }
}