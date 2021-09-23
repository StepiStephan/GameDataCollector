using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandling
{
    public class StorrageHandler : IStorrageHandler
    {
        public void AddGame(string storageId, Game game)
        {
            throw new NotImplementedException();
        }

        public void DeleteGame(string storageId, string gameId)
        {
            throw new NotImplementedException();
        }

        public void EditStorage(string id, string name, float space)
        {
            throw new NotImplementedException();
        }

        public void MoveGame(string fromId, string toId, string gameId)
        {
            throw new NotImplementedException();
        }
    }
}
