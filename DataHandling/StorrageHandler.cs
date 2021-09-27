using DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataHandling
{
    public class StorrageHandler : IStorrageHandler
    {
        private Storage storage;
        public void AddGame(Game game)
        {
            var containingGame = storage.Games.Where(x => x.Id == game.Id).FirstOrDefault();
            if (containingGame == null)
            {
                storage.AddGame(game);
            }
        }

        public void DeleteGame(string gameId)
        {
            var containingGame = storage.Games.Where(x => x.Id == gameId).FirstOrDefault();
            if (containingGame != null)
            {
                storage.Games.Remove(containingGame);
            }
        }

        public void EditStorage(string name, float space)
        {
            if(name == string.Empty)
            {
                name = storage.Name;
            }
            if(space == 0f)
            {
                space = storage.Space;
            }

            var newStorage = new Storage(space, name);

            foreach (var game in storage.Games)
            {
                newStorage.AddGame(game);
            }

            storage = newStorage;
        }

        public Game GetGame(string id)
        {
            var resultgame = storage.Games.Where(x => x.Id == id).FirstOrDefault();
            return resultgame;
        }
    }
}
