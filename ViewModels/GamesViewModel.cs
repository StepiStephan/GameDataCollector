using DataClasses;
using Enums;
using GameDataCollectorWorkflow.Contract;
using System.Collections.Generic;
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

        public void AddGenre(string gameId, List<Genre> genre)
        {
            workflow.AddGenre(gameId, genre);
        }

        public Game CreateGame(string storageId, string name, List<Genre> genres, float space)
        {
            return workflow.CreateGame(storageId, name, genres, space);
        }

        public void DeleteGame(string gameId)
        {
            workflow.DeleteGame(gameId);
        }

        public void DeleteGenre(string gameId, Genre genre)
        {
            workflow.DeleteGenre(gameId, genre);
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
    }
}