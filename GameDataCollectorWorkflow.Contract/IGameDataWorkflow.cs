using Enums;
using System;
using System.Collections.Generic;

namespace GameDataCollectorWorkflow.Contract
{
    public interface IGameDataWorkflow
    {
        string GenerateGame(string name, float size, List<Genre> genres);
        string GenerateConsole(string name, float size, List<Genre> genres);
        string GenerateStorage(string name, float size, List<Genre> genres);
        void AddGameToStorage();
        void AddStorgeToConsole();
        void DeleteGame();
        void DeleteStorage();
        void DeleteConsole();
        void EditGame();
        void EditStorage();
        string GetGame();
        string GetStorage();
        string GetConsole();
    }
}
