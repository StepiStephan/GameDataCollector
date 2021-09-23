using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandling
{
    public interface IStorrageHandler
    {
        void EditStorage(string id, string name, float space);
        void DeleteGame(string storageId, string gameId);
        void AddGame(string storageId, Game game);
        void MoveGame(string fromId, string toId, string gameId);
        Game GetGame(string id, string gameName);
    }
}
