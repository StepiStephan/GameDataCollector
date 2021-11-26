﻿using DataClasses;
using Enums;
using System.Collections.Generic;

namespace DataManaging.Contract
{
    public interface IGameManager
    {
        IEnumerable<Game> Games { get; }
        void AddGame(string storageId, Game game, IKonsoleManager konsoleManager, IStorageManager storageManager);
        void AddGenre(string gameId, List<Genre> genre);
        void DeleteGame(string gameId, IStorageManager storageManager);
        void DeleteGenre(string gameId, Genre genre);
        void EditGame(string gameId, string name, float space);
        Game GetGame(string gameId);
        void SaveData();
        Game CreateGame(string gameId, string name, float space, IEnumerable<Genre> genres);
        Game Copy(Game game);
        void DeleteDescripton(string gameId, Descriptor descriptor);
        void AddDescripton(string gameId, List<Descriptor> descriptor);
    }
}