using DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataHandling
{
    public interface IStorrageHandler
    {
        string ID { get; }
        void EditStorage(string name, float space);
        void DeleteGame(string gameId);
        void AddGame(Game game);
        Game GetGame(string gameId);
    }
}
