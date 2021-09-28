using DataClasses;
using DataManaging.Contract;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging
{
    public class DataManager : IDataManager
    {
        private readonly List<Game> games;
        private readonly List<Storage> storages;
        private readonly List<Konsole> konsoles;

        private const string gameName = "GameList";
        private const string storageName = "StorageList";
        private const string konsoleName = "KonsoleList";

        private readonly IDataSaver<List<Game>> dataSaverGame = new DataSaver<List<Game>>(gameName);
        private readonly IDataSaver<List<Storage>> dataSaverStorage = new DataSaver<List<Storage>>(storageName);
        private readonly IDataSaver<List<Konsole>> dataSaverKonsole = new DataSaver<List<Konsole>>(konsoleName);
        private readonly IDataLoader<List<Game>> dataLoaderGame = new DataLoader<List<Game>>(gameName);
        private readonly IDataLoader<List<Storage>> dataLoaderStorage = new DataLoader<List<Storage>>(storageName);
        private readonly IDataLoader<List<Konsole>> dataLoaderKonsole = new DataLoader<List<Konsole>>(konsoleName);

        public List<Konsole> Konsolen { get => konsoles; }

        public DataManager()
        {
            games = dataLoaderGame.LoadObject();
            storages = dataLoaderStorage.LoadObject();
            konsoles = dataLoaderKonsole.LoadObject();

            if (games == null)
            {
                games = new List<Game>();
                dataSaverGame.SaveObject(games);
            }
            if (storages == null)
            {
                storages = new List<Storage>();
                dataSaverStorage.SaveObject(storages);
            }
            if (konsoles == null)
            {
                konsoles = new List<Konsole>();
                dataSaverKonsole.SaveObject(konsoles);
            }
        }
        public void AddGame(string storageId, Game game)
        {
            foreach (var konsole in konsoles)
            {
                var containigStorageId = konsole.Storages.Where(x => x == storageId).FirstOrDefault();
                if (containigStorageId != null)
                {
                    var storage = storages.Where(x => x.Id == containigStorageId).First();
                    storage.AddGame(game.Id);
                    games.Add(game);
                }
            }
        }
        public void AddGenre(string gameId, List<Genre> genre)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                game.GameGenre.AddRange(genre);
            }
        }
        public void AddKonsole(Konsole konsole)
        {
            konsoles.Add(konsole);
        }
        public void AddStorage(string konsoleId, Storage storage)
        {
            var konsole = konsoles.Where(x => x.Id == konsoleId).FirstOrDefault();
            if (konsole != null)
            {
                konsole.AddStorage(storage.Id);
                storages.Add(storage);
            }
        }
        public Game CreateGame(string storageId, string name, List<Genre> genres, float space)
        {
            var game = new Game(name, genres, space);
            AddGame(storageId, game);

            return game;
        }
        public Storage CreateStorage(string konsoleId, string name, float space)
        {
            var storage = new Storage(space, name);
            AddStorage(konsoleId, storage);

            return storage;
        }
        public Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher)
        {
            var storage = new Storage(internerSpeicher, "Interner Speicher von " + konsoleName);
            var konsole = new Konsole(konsoleName, name);

            AddKonsole(konsole);
            AddStorage(konsole.Id, storage);

            return konsole;
        }
        public void DeleteGame(string gameId)
        {
            var storage = storages.Where(x => x.Games.Where(y => y == gameId).FirstOrDefault() != null).FirstOrDefault();
            if (storage != null)
            {
                storage.Games.Remove(gameId);
            }
            games.Remove(GetGame(gameId));
        }
        public void DeleteGenre(string gameId, Genre genre)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                game.GameGenre.Remove(genre);
            }
        }
        public void EditGame(string gameId, string name, float space)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                if (name == null || name == string.Empty)
                {
                    name = game.Name;
                }
                if (space == 0f)
                {
                    space = game.SpaceOnSorage;
                }

                var newGame = new Game(name, game.GameGenre, space, game.Id);
                games.Remove(game);
                games.Add(newGame);
            }
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
        public Game GetGame(string gameId)
        {
            return games.Where(x => x.Id == gameId).FirstOrDefault();
        }
        public Storage GetStorage(string storageId)
        {
            return storages.Where(x => x.Id == storageId).FirstOrDefault();
        }
        public void RanameKonsole(string konsoleId, string name)
        {
            var konsole = konsoles.Where(x => x.Id == konsoleId).FirstOrDefault();
            if (konsole != null)
            {
                var newKonsole = new Konsole(konsole.ConsoleName, name, konsole.Id);
                foreach (var storage in konsole.Storages)
                {
                    newKonsole.AddStorage(storage);
                }
            }
        }

        public void SaveData()
        {
            dataSaverGame.SaveObject(games);
            dataSaverKonsole.SaveObject(konsoles);
            dataSaverStorage.SaveObject(storages);
        }
    }
}
