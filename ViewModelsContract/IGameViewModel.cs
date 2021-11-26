using DataClasses;
using Enums;
using System.Collections.Generic;

namespace ViewModels.Contract
{
    public interface IGameViewModel
    {
        List<Game> Games { get; }
        Konsole SelectedKonsole { get; set; }
        Storage SelectedStorage { get; set; }

        Game CreateGame(string storageId, string name, List<Genre> genres, float space);
        void DeleteGame(string gameId);
        void DeleteGenre(string gameId, Genre genre);
        void EditGame(string gameId, string name, float space);
        Game GetGame(string gameId);
        void AddGenre(string gameId, List<Genre> genre);
        string[] GetInfos(Game game);
        List<Game> GetGames();
        List<Storage> GetStorages();
        List<Konsole> GetKonsolen();

        void MoveGame(string oldStorageId, string gameId, string newStorageId);
        void AddDescriptors(string id, List<Descriptor> selectedDescriptors);
        void DeleteDescription(string gameId, Descriptor descriptor);
    }
}