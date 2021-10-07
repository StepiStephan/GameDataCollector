using DataClasses;
using Enums;
using System.Collections.Generic;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IGameDataWorkflow
    {
        List<Konsole> Konsolen { get; }
        List<Storage> Storages { get; }
        List<Game> Games { get; }

        void AddGame(string storageId, Game game);
        void AddGenre(string gameId, List<Genre> genre);
        void AddKonsole(Konsole konsole);
        void AddStorage(string konsoleId, Storage storage);
        Game CreateGame(string storageId, string name, List<Genre> genres, float space);
        Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher);
        Storage CreateStorage(string konsoleId, string name, float space);
        void DeleteGame(string gameId);
        void DeleteGenre(string gameId, Genre genre);
        void DeleteKonsole(string id);
        void DeleteKonsoleWithAllGames(string id);
        void DeleteKonsoleWithAllStorages(string id);
        void DeleteStorage(string id);
        void DeleteStorageWithGames(string id);
        void EditGame(string gameId, string name, float space);
        void EditKonsole(string konsoleId, string name, string consoleName);
        void EditStorage(string storageId, string name, float space);
        Game GetGame(string gameId);
        Konsole GetKonsole(string konsoleId);
        Storage GetStorage(string storageId);
        void SaveData();
    }
}