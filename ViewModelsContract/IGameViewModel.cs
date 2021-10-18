using DataClasses;
using Enums;
using System.Collections.Generic;

namespace ViewModels.Contract
{
    public interface IGameViewModel
    {
        List<Game> Games { get; }
        Game CreateGame(string storageId, string name, List<Genre> genres, float space);
        void DeleteGame(string gameId);
        void DeleteGenre(string gameId, Genre genre);
        void EditGame(string gameId, string name, float space);
        Game GetGame(string gameId);
        void AddGenre(string gameId, List<Genre> genre);

        List<Game> GetGames();
    }
}