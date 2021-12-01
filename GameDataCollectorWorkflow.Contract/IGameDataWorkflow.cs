using DataClasses;
using Enums;
using System.Collections.Generic;
using System.ComponentModel;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IGameDataWorkflow : INotifyPropertyChanged
    {
        List<Konsole> Konsolen { get; set; }
        List<Storage> Storages { get; set; }
        List<Game> Games { get; set; }

        string SelectedKonsole { get; }
        string SelectedStorage { get; }

        void SetIFirebaseWorkflow(IFireBaseWorkFlow fireBaseWorkFlow);
        void ClearSelecion();
        void AddGame(string storageId, Game game);
        void AddGenre(string gameId, List<Genre> genre);
        void DeleteDescriptor(string gameId, Descriptor descriptor);
        void AddDescriptors(string gameId, List<Descriptor> descriptors);
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
        void SelectKonsole(string konsoleId);
        void SelectStorage(string storageId);
        List<Storage> GetStorages();
        List<Game> GetGames();
        void SaveData();
        void MoveGame(string oldStorageId, string gameId, string newStorageId);
        void MoveStorage(string oldKonsoleId, string storageId, string newKonsoleId);
    }
}