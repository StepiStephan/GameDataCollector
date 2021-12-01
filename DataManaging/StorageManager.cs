﻿using DataClasses;
using DataManaging.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging
{
    public class StorageManager : IStorageManager
    {
        private List<Storage> storages;
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
        public IEnumerable<Storage> Storages 
        { 
            get => storages;
            set => storages = value.ToList();
        }

        public void SaveData()
        {
            dataSaverStorage.SaveObject(storages);
        }

        public void DeleteStorageWithGames(string id, IGameManager gameManager)
        {
            var storage = storages.Where(x => x.Id == id).FirstOrDefault();

            if (storage != null && storage.Games != null)
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

                var newStorage = CreateStorage(space, name, storage.Id);
                foreach (var gameId in storage.Games)
                {
                    newStorage.Games.Add(gameId);
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
                konsole.Storages.Add(storage.Id);
                storages.Add(storage);
            }
        }
        public Storage CreateStorage(float space, string name, string id)
        {
            return new Storage()
            {
                Id = id,
                Name = name,
                Space = space,
                Games = new List<string>()
            };
        }
        public Storage CreateStorage(float space, string name)
        {
            var id = Guid.NewGuid().ToString();
            return CreateStorage(space, name, id);
        }
        public Storage Copy(string storageId)
        {
            var storage = GetStorage(storageId);
            var result = CreateStorage(storage.Space, storage.Name, storage.Id);

            foreach (var gameID in storage.Games)
            {
                result.Games.Add(gameID);
            }

            return result;
        }
    }
}