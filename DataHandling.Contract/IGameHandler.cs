using DataClasses;
using System;
using System.Collections.Generic;

namespace DataHandling
{
    public interface IGameHandler
    {
        void EditGame(string id, string name, float space);
        void AddGenre(string id, List<Genre> genre);
        void DeleteGenre(string id, Genre genre);
    }
}
