using System;
using System.Collections.Generic;
using Enums;

namespace DataClasses
{
    public class Game
    {
        public string Name { get; set; }
        public float SpaceOnSorage { get; set; }
        public string Id { get; set; }
        public List<Genre> GameGenre { get; set; }
    }
}