using DataClasses;
using DataManaging;
using DataManaging.Contract;
using Enums;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace GameDataCollectorWorkflow
{
    public class GameDataWorkflow : IGameDataWorkflow
    {
        private readonly IGameManager gameManager;
        private readonly IStorageManager storageManager;
        private readonly IKonsoleManager konsoleManager;
        public GameDataWorkflow(IGameManager gameManager, IStorageManager storageManager, IKonsoleManager konsoleManager)
        {
            this.gameManager = gameManager;
            this.storageManager = storageManager;
            this.konsoleManager = konsoleManager;
        }

        public List<Konsole> Konsolen => konsoleManager.Konsoles.ToList();

        public List<Storage> Storages => storageManager.Storages.ToList();

        public List<Game> Games => gameManager.Games.ToList();

        public Game CreateGame(string storageId, string name, List<Genre> genres, float space)
        {
            var game = new Game(name, genres, space);
            gameManager.AddGame(storageId, game, konsoleManager, storageManager);

            return game;
        }
        public Storage CreateStorage(string konsoleId, string name, float space)
        {
            var storage = new Storage(space, name);
            storageManager.AddStorage(konsoleId, storage, konsoleManager);

            return storage;
        }
        public Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher)
        {
            var storage = new Storage(internerSpeicher, "Interner Speicher von " + konsoleName);
            var konsole = new Konsole(konsoleName, name);

            konsoleManager.AddKonsole(konsole);
            storageManager.AddStorage(konsole.Id, storage, konsoleManager);

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
        }

        public void AddGenre(string gameId, List<Genre> genre)
        {
            gameManager.AddGenre(gameId, genre);
        }

        public void AddKonsole(Konsole konsole)
        {
            konsoleManager.AddKonsole(konsole);
        }

        public void AddStorage(string konsoleId, Storage storage)
        {
            storageManager.AddStorage(konsoleId, storage, konsoleManager);
        }

        public void DeleteGame(string gameId)
        {
            gameManager.DeleteGame(gameId, storageManager);
        }

        public void DeleteGenre(string gameId, Genre genre)
        {
            gameManager.DeleteGenre(gameId, genre);
        }

        public void DeleteKonsoleWithAllStorages(string id)
        {
            konsoleManager.DeleteKonsoleWithAllStorages(id, storageManager);
        }

        public void DeleteKonsoleWithAllGames(string id)
        {
            konsoleManager.DeleteKonsoleWithAllGames(id, storageManager, gameManager);
        }

        public void DeleteKonsole(string id)
        {
            konsoleManager.DeleteKonsole(id, storageManager, gameManager);
        }

        public void DeleteStorageWithGames(string id)
        {
            storageManager.DeleteStorageWithGames(id, gameManager);
        }

        public void DeleteStorage(string id)
        {
            storageManager.DeleteStorage(id);
        }

        public void EditGame(string gameId, string name, float space)
        {
            gameManager.EditGame(gameId, name, space);
        }

        public void EditStorage(string storageId, string name, float space)
        {
            storageManager.EditStorage(storageId, name, space);
        }

        public void EditKonsole(string konsoleId, string name, string consoleName)
        {
            konsoleManager.EditKonsole(konsoleId, consoleName, name);
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
    }
}
