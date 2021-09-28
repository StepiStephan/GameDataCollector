using DataClasses;
using DataManaging;
using DataManaging.Contract;
using Enums;
using GameDataCollectorWorkflow;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Linq;

namespace TestKonsole
{
    class Program
    {
        static void Main(string[] args)
        {
            IDataManager dataManager = new DataManager();
            var konsole = dataManager.CreateKonsole("Switch", "Alte Switch", 32);
            dataManager.CreateGame(konsole.Storages.First(), "Zelda BOTW", new List<Genre>() { Genre.SingelPlayer }, 7);
            var storage = dataManager.GetStorage(konsole.Storages.First());
            var game = dataManager.GetGame(storage.Games.First());            
            
            Console.Write(konsole);
            Console.Write(storage);
            Console.Write(game);
            Console.Read();

        }
    }
}
