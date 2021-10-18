using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace ViewModels.Contract
{
    public interface IStorageViewModel
    {
        List<Storage> Storages { get; }
        void AddGame(string storageId, Game game);
        Storage CreateStorage(string konsoleId, string name, float space);
        void DeleteStorage(string id);
        void DeleteStorageWithGames(string id);
        void EditStorage(string storageId, string name, float space);
        Storage GetStorage(string storageId);
        List<Storage> GetStorages();
        void SetStorage(string id);

    }
}
