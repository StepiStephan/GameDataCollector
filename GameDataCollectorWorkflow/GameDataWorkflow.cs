using DataClasses;
using DataManaging.Contract;
using Enums;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace GameDataCollectorWorkflow
{
    public class GameDataWorkflow : IGameDataWorkflow
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler DataChanged;

        private readonly IGameManager gameManager;
        private readonly IStorageManager storageManager;
        private readonly IKonsoleManager konsoleManager;
        private IFireBaseWorkFlow fireBaseWorkFlow;

        private string selectedKonsoleId;
        private string selectedStorageID;

        public List<Konsole> Konsolen
        {
            get => konsoleManager.Konsoles.ToList();
            set => konsoleManager.Konsoles = value.AsEnumerable();
        }
        public List<Storage> Storages 
        { 
            get => storageManager.Storages.ToList();
            set => storageManager.Storages = value.AsEnumerable();
        }
        public List<Game> Games
        {
            get => gameManager.Games.ToList();
            set => gameManager.Games = value.AsEnumerable();
        }
        public string SelectedKonsole => selectedKonsoleId;
        public string SelectedStorage => selectedStorageID;

        public GameDataWorkflow(IGameManager gameManager, IStorageManager storageManager, IKonsoleManager konsoleManager)
        {
            this.gameManager = gameManager;
            this.storageManager = storageManager;
            this.konsoleManager = konsoleManager;
            DataChanged += GameDataWorkflow_DataChanged;
        }

        private void FireBaseWorkFlow_DatabaseSaved(object sender, EventArgs e)
        {
            OnDataChange();
        }

        private void FireBaseWorkFlow_DatabaseLoaded(object sender, EventArgs e)
        {
            OnDataChange();
        }

        private void GameDataWorkflow_DataChanged(object sender, EventArgs e)
        {
            SaveData();
        }

        protected void OnDataChange()
        {
            DataChanged?.Invoke(this, EventArgs.Empty);
        }

        public Game CreateGame(string storageId, string name, List<Genre> genres, float space)
        {
            var game = gameManager.CreateGame(Guid.NewGuid().ToString(), name, space, genres);
            gameManager.AddGame(storageId, game, konsoleManager, storageManager);
            OnDataChange();
            return game;
        }
        public Storage CreateStorage(string konsoleId, string name, float space)
        {
            var storage = storageManager.CreateStorage(space, name);
            storageManager.AddStorage(konsoleId, storage, konsoleManager);
            OnDataChange();
            return storage;
        }
        public Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher)
        {
            var storage = storageManager.CreateStorage(internerSpeicher, "Interner Speicher von " + name);
            var konsole = konsoleManager.CreateKonsole(konsoleName, name);

            konsoleManager.AddKonsole(konsole);
            storageManager.AddStorage(konsole.Id, storage, konsoleManager);
            OnDataChange();
            return konsole;
        }
        public void SaveData()
        {
            gameManager.SaveData();
            konsoleManager.SaveData();
            storageManager.SaveData();
        }

        public void AddGame(string storageId, Game game)
        {
            gameManager.AddGame(storageId, game, konsoleManager, storageManager);
            OnDataChange();
        }

        public void AddGenre(string gameId, List<Genre> genre)
        {
            gameManager.AddGenre(gameId, genre);
            OnDataChange();
        }

        public void AddDescriptors(string gameId, List<Descriptor> descriptors)
        {
            gameManager.AddDescripton(gameId, descriptors);
            OnDataChange();
        }

        public void AddKonsole(Konsole konsole)
        {
            konsoleManager.AddKonsole(konsole);
            OnDataChange();
        }

        public void AddStorage(string konsoleId, Storage storage)
        {
            storageManager.AddStorage(konsoleId, storage, konsoleManager);
            OnDataChange();
        }

        public void DeleteGame(string gameId)
        {
            gameManager.DeleteGame(gameId, storageManager);
            OnDataChange();
        }

        public void DeleteDescriptor(string gameId, Descriptor descriptor)
        {
            gameManager.DeleteDescripton(gameId, descriptor);
            OnDataChange();
        }

        public void DeleteGenre(string gameId, Genre genre)
        {
            gameManager.DeleteGenre(gameId, genre);
            OnDataChange();
        }

        public void DeleteKonsoleWithAllStorages(string id)
        {
            konsoleManager.DeleteKonsoleWithAllStorages(id, storageManager);
            OnDataChange();
        }

        public void DeleteKonsoleWithAllGames(string id)
        {
            konsoleManager.DeleteKonsoleWithAllGames(id, storageManager, gameManager);
            OnDataChange();
        }

        public void DeleteKonsole(string id)
        {
            konsoleManager.DeleteKonsole(id, storageManager, gameManager);
            OnDataChange();
        }

        public void DeleteStorageWithGames(string id)
        {
            storageManager.DeleteStorageWithGames(id, gameManager);
            OnDataChange();
        }

        public void DeleteStorage(string id)
        {
            storageManager.DeleteStorage(id);
            OnDataChange();
        }

        public void EditGame(string gameId, string name, float space)
        {
            gameManager.EditGame(gameId, name, space);
            OnDataChange();
        }

        public void EditStorage(string storageId, string name, float space)
        {
            storageManager.EditStorage(storageId, name, space);
            OnDataChange();
        }

        public void EditKonsole(string konsoleId, string name, string consoleName)
        {
            konsoleManager.EditKonsole(konsoleId, consoleName, name);
            OnDataChange();
        }

        public Game GetGame(string gameId)
        {
            return gameManager.GetGame(gameId);
        }

        public Storage GetStorage(string storageId)
        {
            return storageManager.GetStorage(storageId);
        }

        public Konsole GetKonsole(string konsoleId)
        {
            return konsoleManager.GetKonsole(konsoleId);
        }

        public void ClearSelecion()
        {
            selectedKonsoleId = null;
            selectedStorageID = null;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedIDs"));
        }
        public void SelectKonsole(string konsoleId)
        {
            selectedKonsoleId = konsoleId;
            selectedStorageID = null;
        }

        public void SelectStorage(string storageId)
        {
            selectedStorageID = storageId;
        }

        public List<Storage> GetStorages()
        {
            if(selectedKonsoleId == null)
                return Storages;
            var konsole = GetKonsole(selectedKonsoleId);
            if(konsole == null)
            {
                return Storages;
            }
            return Storages.Where(x => konsole.Storages.Contains(x.Id)).ToList();
        }

        public List<Game> GetGames()
        {
            List<Game> games = new List<Game>();
            if (selectedStorageID == null && selectedKonsoleId == null)
                games = Games;
            else
            {
                if(selectedStorageID == null && selectedKonsoleId != null)
                {
                    var konsole = GetKonsole(selectedKonsoleId);
                    var result = new List<Game>();
                    foreach (var storageID in konsole.Storages)
                    {
                        result.AddRange(Games.Where(x => GetStorage(storageID).Games.Contains(x.Id)));
                    }
                    games = result;
                }
                else
                {
                    var storage = GetStorage(selectedStorageID);
                    games = Games.Where(x => storage.Games.Contains(x.Id)).ToList();
                }
            }

            return games.OrderBy(x => x.Name).ToList();
        }

        public void MoveGame(string oldStorageId, string gameId, string newStorageId)
        {
            var oldStorage = storageManager.GetStorage(oldStorageId);
            var newStorage = storageManager.GetStorage(newStorageId);
            var game = gameManager.GetGame(gameId);

            if(oldStorage != null && newStorage != null && game != null)
            {
                oldStorage.Games.Remove(game.Id);
                newStorage.Games.Add(game.Id);
                OnDataChange();
            }
        }

        public void MoveStorage(string oldKonsoleId, string storageId, string newKonsoleId)
        {
            var oldKonsole = konsoleManager.GetKonsole(oldKonsoleId);
            var newKonsole = konsoleManager.GetKonsole(newKonsoleId);
            var storage = storageManager.GetStorage(storageId);

            if (oldKonsole != null && newKonsole != null && storage != null)
            {
                oldKonsole.Storages.Remove(storage.Id);
                newKonsole.Storages.Add(storage.Id);
                OnDataChange();
            }
        }

        public void SetIFirebaseWorkflow(IFireBaseWorkFlow fireBaseWorkFlow)
        {
            this.fireBaseWorkFlow = fireBaseWorkFlow;

            fireBaseWorkFlow.DatabaseLoaded += FireBaseWorkFlow_DatabaseLoaded;
            fireBaseWorkFlow.DatabaseSaved += FireBaseWorkFlow_DatabaseSaved;
        }
    }
}
