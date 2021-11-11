using DataClasses;
using System.Collections.Generic;

namespace DataManaging.Contract
{
    public interface IStorageManager
    {
        IEnumerable<Storage> Storages { get; }
        void AddStorage(string konsoleId, Storage storage, IKonsoleManager konsoleManager);
        void DeleteStorage(string id);
        void DeleteStorageWithGames(string id, IGameManager gameManager);
        void EditStorage(string storageId, string name, float space);
        Storage GetStorage(string storageId);
        void SaveData();
        Storage CreateStorage(float internerSpeicher, string v);
    }
}