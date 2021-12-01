using DataClasses;
using System.Collections.Generic;

namespace DataManaging.Contract
{
    public interface IKonsoleManager
    {
        IEnumerable<Konsole> Konsoles { get; set; }
        void AddKonsole(Konsole konsole);
        void DeleteKonsole(string id, IStorageManager storageManager, IGameManager gameManager);
        void DeleteKonsoleWithAllGames(string id, IStorageManager storageManager, IGameManager gameManager);
        void DeleteKonsoleWithAllStorages(string id, IStorageManager storageManager);
        void EditKonsole(string konsoleId, string consoleName, string name);
        Konsole GetKonsole(string konsoleId);
        void SaveData();
        Konsole CreateKonsole(string konsoleName, string name);
    }
}