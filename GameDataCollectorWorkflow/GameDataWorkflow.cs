using Enums;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;

namespace GameDataCollectorWorkflow
{
    public class GameDataWorkflow : IGameDataWorkflow
    {
        public void AddGameToStorage()
        {
            throw new NotImplementedException();
        }

        public void AddStorgeToConsole()
        {
            throw new NotImplementedException();
        }

        public void DeleteConsole()
        {
            throw new NotImplementedException();
        }

        public void DeleteGame()
        {
            throw new NotImplementedException();
        }

        public void DeleteStorage()
        {
            throw new NotImplementedException();
        }

        public void EditGame()
        {
            throw new NotImplementedException();
        }

        public void EditStorage()
        {
            throw new NotImplementedException();
        }

        public string GenerateConsole(string name, float size, List<Genre> genres)
        {
            throw new NotImplementedException();
        }

        public string GenerateGame(string name, float size, List<Genre> genres)
        {
            throw new NotImplementedException();
        }

        public string GenerateStorage(string name, float size, List<Genre> genres)
        {
            throw new NotImplementedException();
        }

        public string GetConsole()
        {
            throw new NotImplementedException();
        }

        public string GetGame()
        {
            throw new NotImplementedException();
        }

        public string GetStorage()
        {
            throw new NotImplementedException();
        }
    }
}
