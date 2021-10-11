using DataClasses;
using DataManaging.Contract;
using Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace DataManaging
{
    public class GameManager : IGameManager
    {
        private readonly List<Game> games;
        private const string gameName = "GameList";
        private readonly IDataSaver<List<Game>> dataSaverGame; //new DataSaver<List<Game>>(gameName);
        private readonly IDataLoader<List<Game>> dataLoaderGame; //new DataLoader<List<Game>>(gameName);

        public IEnumerable<Game> Games => games;

        public void AddGenre(string gameId, List<Genre> genre)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                game.GameGenre.AddRange(genre);
            }
        }

        public GameManager(IDataSaver<List<Game>> dataSaver, IDataLoader<List<Game>> dataLoader)
        {
            dataLoaderGame = dataLoader;
            dataSaverGame = dataSaver;
            dataLoaderGame.SetName(gameName);
            dataSaverGame.SetName(gameName);
            games = dataLoaderGame.LoadObject();

            if (games == null)
            {
                games = new List<Game>();
                dataSaverGame.SaveObject(games);
            }
        }

        public void SaveData()
        {
            dataSaverGame.SaveObject(games);
        }

        public void DeleteGame(string gameId, IStorageManager storageManager)
        {
            var storage = storageManager.Storages.Where(x => x.Games.Where(y => y == gameId).FirstOrDefault() != null).FirstOrDefault();
            if (storage != null)
            {
                storage.Games.Remove(gameId);
            }
            games.Remove(GetGame(gameId));
        }

        public void AddGame(string storageId, Game game, IKonsoleManager konsoleManager, IStorageManager storageManager)
        {
            foreach (var konsole in konsoleManager.Konsoles)
            {
                var containigStorageId = konsole.Storages.Where(x => x == storageId).FirstOrDefault();
                if (containigStorageId != null)
                {
                    var storage = storageManager.Storages.Where(x => x.Id == containigStorageId).First();
                    storage.AddGame(game.Id);
                    games.Add(game);
                }
            }
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

        public Game GetGame(string gameId)
        {
            return games.Where(x => x.Id == gameId).FirstOrDefault();
        }
    }
}
