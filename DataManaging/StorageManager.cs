using DataClasses;
using DataManaging.Contract;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging
{
    public class StorageManager : IStorageManager
    {
        private readonly List<Storage> storages;
        private const string storageName = "StorageList";
        private readonly IDataSaver<List<Storage>> dataSaverStorage;
        private readonly IDataLoader<List<Storage>> dataLoaderStorage;

        public StorageManager(IDataSaver<List<Storage>> dataSaverStorage, IDataLoader<List<Storage>> dataLoaderStorage)
        {
            this.dataLoaderStorage = dataLoaderStorage;
            this.dataSaverStorage = dataSaverStorage;
            this.dataSaverStorage.SetName(storageName);
            this.dataLoaderStorage.SetName(storageName);
            storages = dataLoaderStorage.LoadObject();
            if (storages == null)
            {
                storages = new List<Storage>();
                dataSaverStorage.SaveObject(storages);
            }
        }
        public IEnumerable<Storage> Storages { get => storages; }

        public void SaveData()
        {
            dataSaverStorage.SaveObject(storages);
        }

        public void DeleteStorageWithGames(string id, IGameManager gameManager)
        {
            var storage = storages.Where(x => x.Id == id).FirstOrDefault();

            if (storage != null)
            {
                List<string> gameIds = new List<string>();

                foreach (var game in storage.Games)
                {
                    gameIds.Add(game);
                }

                foreach (var gameId in gameIds)
                {
                    gameManager.DeleteGame(gameId, this);
                }

                storages.Remove(storage);
            }
        }

        public void DeleteStorage(string id)
        {
            var storage = storages.Where(x => x.Id == id).FirstOrDefault();

            if (storage != null)
            {
                storages.Remove(storage);
            }
        }

        public Storage GetStorage(string storageId)
        {
            return storages.Where(x => x.Id == storageId).FirstOrDefault();
        }
        public void EditStorage(string storageId, string name, float space)
        {
            var storage = storages.Where(x => x.Id == storageId).FirstOrDefault();
            if (storage != null)
            {
                if (name == null || name == string.Empty)
                {
                    name = storage.Name;
                }
                if (space == 0f)
                {
                    space = storage.Space;
                }

                var newStorage = new Storage(space, name, storage.Id);
                foreach (var gameId in storage.Games)
                {
                    newStorage.AddGame(gameId);
                }
                storages.Remove(storage);
                storages.Add(newStorage);
            }
        }

        public void AddStorage(string konsoleId, Storage storage, IKonsoleManager konsoleManager)
        {
            var konsole = konsoleManager.Konsoles.Where(x => x.Id == konsoleId).FirstOrDefault();
            if (konsole != null)
            {
                konsole.AddStorage(storage.Id);
                storages.Add(storage);
            }
        }
    }
}