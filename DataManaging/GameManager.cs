using DataClasses;
using DataManaging.Contract;
using Enums;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging
{
    public class GameManager : IGameManager
    {
        private List<Game> games;
        private const string gameName = "GameList";
        private readonly IDataSaver<List<Game>> dataSaverGame; 
        private readonly IDataLoader<List<Game>> dataLoaderGame;

        public IEnumerable<Game> Games
        {
            get => games.OrderBy(x => x.Name);
            set => games = value.ToList();

        }
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
                    storage.Games.Add(game.Id);
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

        public void DeleteDescripton(string gameId, Descriptor descriptor)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                game.GameDiscriptors.Remove(descriptor);
            }
        }
        public void AddDescripton(string gameId, List<Descriptor> descriptor)
        {
            var game = games.Where(x => x.Id == gameId).FirstOrDefault();
            if (game != null)
            {
                game.GameDiscriptors.AddRange(descriptor);
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

                var newGame = CreateGame(game.Id, name, space, game.GameGenre);

                newGame.GameDiscriptors = game.GameDiscriptors;
                games.Remove(game);
                games.Add(newGame);
            }
        }

        public Game GetGame(string gameId)
        {
            return games.Where(x => x.Id == gameId).FirstOrDefault();
        }

        public Game CreateGame(string gameId, string name, float space, IEnumerable<Genre> genres)
        {
            return new Game()
            {
                Name = name,
                GameGenre = genres.ToList(),
                SpaceOnSorage = space,
                Id = gameId
            };
        }
        public Game Copy(Game game)
        {
            return CreateGame(game.Id, game.Name, game.SpaceOnSorage, game.GameGenre);
        }
    }
}
