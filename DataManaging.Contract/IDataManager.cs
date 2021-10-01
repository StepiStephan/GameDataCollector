using DataClasses;
using Enums;
using System;
using System.Collections.Generic;

namespace DataManaging.Contract
{
    public interface IDataManager
    {
        List<Konsole> Konsolen { get; }
        void AddGame(string storageId, Game game);
        void AddGenre(string gameId, List<Genre> genre);
        void AddKonsole(Konsole konsole);
        void AddStorage(string konsoleId, Storage storage);
        Game CreateGame(string storageId, string name, List<Genre> genres, float space);
        Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher);
        Storage CreateStorage(string konsoleId, string name, float space);
        void DeleteGame(string gameId);
        void DeleteGenre(string gameId, Genre genre);
        void DeleteKonsoleWithAllStorages(string id);
        void DeleteKonsoleWithAllGames(string id);
        void DeleteKonsole(string id);
        void DeleteStorageWithGames(string id);
        void DeleteStorage(string id);
        void EditGame(string gameId, string name, float space);
        void EditStorage(string storageId, string name, float space);
        void EditKonsole(string storageId, string name, string consoleName);
        Game GetGame(string gameId);
        Storage GetStorage(string storageId);
        Konsole GetKonsole(string KonsoleId);
        void SaveData();
    }
}
