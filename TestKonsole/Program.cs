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
            
            FillWithNewData(dataManager);
            PrintAllData(dataManager);

            Console.Read();
        }

        private static void FillWithNewData(IDataManager dataManager)
        {
            var konsoleToAdd = dataManager.Konsolen.First();
            var kart128 = dataManager.CreateStorage(konsoleToAdd.Id, "Multiplayer Karte", 128);
            dataManager.CreateGame(konsoleToAdd.Storages.First(), "DOOM 2016", new List<Genre> { Genre.SingelPlayer, Genre.Egoshooter }, 20);
            dataManager.CreateGame(kart128.Id, "Mario Party", new List<Genre>() { Genre.Coop, Genre.Party }, 5);
        }

        private static void PrintAllData(IDataManager dataManager)
        {
            foreach (var konsole in dataManager.Konsolen)
            {
                Console.Write(konsole);
                foreach (var storageId in konsole.Storages)
                {
                    var storage = dataManager.GetStorage(storageId);
                    Console.Write("\t" + storage);
                    foreach (var gameId in storage.Games)
                    {
                        var game = dataManager.GetGame(gameId);
                        Console.Write(" \t\t" + game);
                    }
                }
            }
        }
    }
}
