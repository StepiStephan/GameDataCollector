using DataClasses;
using GameDataCollectorWorkflow;
using GameDataCollectorWorkflow.Contract;
using System;
using System.Linq;

namespace TestKonsole
{
    class Program
    {
        static void Main(string[] args)
        {

            Konsole konsole = new Konsole("Switch", "SwitchMH", 32f);
            Storage storage = new Storage(36f, "InternerSpeicher");
            Game game = new Game(
                "Zelda:BotW",
                new System.Collections.Generic.List<Genre>()
                {
                    Genre.SingelPlayer
                },
                16f);
            konsole.AddStorage(storage);
            storage.AddGame(game);

            var konsoleString = konsole.ToString();
            Console.Write(konsoleString);
            Console.Read();

        }
    }
}
