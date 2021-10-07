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
        static void Main()
        {
            IGameDataWorkflow dataManager = new GameDataWorkflow();

            RunConsole(dataManager);
            //FillWithNewData(dataManager);
            //PrintAllData(dataManager);

            Console.Read();
        }

        private static void RunConsole(IGameDataWorkflow dataManager)
        {
            bool run = true;
            while (run)
            {
                var input = Console.ReadLine().Replace(Environment.NewLine, "").Replace(" ", "");

                switch (input)
                {
                    case "--help":
                        HelpContent();
                        break;

                    case "--nc":
                        CreateConsole(dataManager);
                        break;

                    case "--ns":

                        CreateStorage(dataManager);
                        break;

                    case "--ng":

                        CreateGame(dataManager);
                        break;

                    case "save":
                        dataManager.SaveData();
                        break;

                    case "--uk":
                        EditKonsole(dataManager);
                        break;

                    case "--us":
                        EditStorage(dataManager);
                        break;

                    case "--ug":
                        GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
                        if (success)
                        {
                            dataManager.EditGame();
                        }
                        break;

                    case "--dkc":
                        dataManager.DeleteKonsoleWithAllGames();
                        break;
                    case "--dks":
                        dataManager.DeleteKonsoleWithAllStorages();
                        break;

                    case "--dk":
                        dataManager.DeleteKonsole();
                        break;

                    case "--ds":
                        dataManager.DeleteStorage();
                        break;
                    case "--dsc":
                        dataManager.DeleteStorageWithGames();
                        break;

                    case "--dg":
                        dataManager.DeleteGame();
                        break;

                    case "--ec":
                        run = false;
                        break;

                    case "--pd":
                        PrintAllData(dataManager);
                        break;
                    default:
                        Console.WriteLine("Unbekannter Befehl");
                        break;
                }
            }
        }

        private static void EditStorage(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    float space;
                    var name = ConsoleAbfrage("Name eingeben, falls nicht zu ändern leer lassen:");
                    var spaceString = ConsoleAbfrage("Speicer eingeben, falls nicht zu ändern leer lassen:");
                    if (spaceString == "")
                    {
                        space = 0;
                    }
                    else
                    {
                        float.TryParse(spaceString, out space);
                    }
                    dataManager.EditStorage(dataManager.Storages[storageNumber].Id, name, space);
                }
            }
        }

        private static void EditKonsole(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int number);
            if (success)
            {
                var konsole = dataManager.Konsolen[number];
                var name = ConsoleAbfrage("Name eingeben, falls nicht zu ändern leer lassen:");
                var konsoleName = ConsoleAbfrage("KonsoleName eingeben, falls nicht zu ändern leer lassen:");
                dataManager.EditKonsole(konsole.Id, name, konsoleName);
            }
        }

        private static void CreateGame(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    var name = ConsoleAbfrage("Name: ");
                    var spaceSuccess = float.TryParse(ConsoleAbfrage("Space: "), out float space);
                    if (spaceSuccess)
                    {
                        List<Genre> genres = GetGenres();
                        dataManager.CreateGame(dataManager.Storages[storageNumber].Id, name, genres, space);
                    }
                }
            }
        }

        private static List<Genre> GetGenres()
        {
            List<Genre> genres = new List<Genre>();
            bool run = true;
            for(int i = 0; i <= 9; i++)
            {
                Console.WriteLine( $"{i} => " + (Genre)i);
            }
            Console.WriteLine(Environment.NewLine + "--q => zum beenden der Genre");
            while(run)
            {
                var input = Console.ReadLine().Replace(Environment.NewLine, "").Replace(" ", "");
                if(input == "--q")
                {
                    run = false;
                }
                else
                {
                    bool success = int.TryParse(input, out int genreNumber);
                    if(success)
                    {
                        genres.Add((Genre)genreNumber);
                    }
                }
            }
            return genres;
        }

        private static void CreateStorage(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool succsess, out int konsoleNumber);
            if (succsess)
            {
                var succsessSpace = float.TryParse("", out float space);
                if (succsessSpace)
                {
                    var name = ConsoleAbfrage("Speichername: ");
                    dataManager.CreateStorage(dataManager.Konsolen[konsoleNumber].Id, name, space);
                }
            }
        }

        private static void GetKonsoleNumber(IGameDataWorkflow dataManager, out bool succsess, out int konsoleNumber)
        {
            for (int i = 0; i < dataManager.Konsolen.Count; i++)
            {
                var konsole = dataManager.Konsolen[i];
                Console.WriteLine($"[{i}] => KonsolenName: {konsole.ConsoleName} , Name: {konsole.Name}");
            }
            var konsoleNumberString = ConsoleAbfrage("Wähle eine Konsole: ");
            succsess = int.TryParse(konsoleNumberString, out konsoleNumber);
        }

        private static void GetStorageNumber(IGameDataWorkflow dataManager, string konsoleID, out bool succsess, out int konsoleNumber)
        {
            var konsole = dataManager.GetKonsole(konsoleID);

            for (int i = 0; i < dataManager.Storages.Count; i++)
            {
                var storage = dataManager.Storages[i];
                if (konsole.Storages.Contains(storage.Id))
                {
                    Console.WriteLine($"[{i}] => StorageName: {konsole.ConsoleName}");
                }
            }
            var konsoleNumberString = ConsoleAbfrage("Wähle ein Speicher: ");
            succsess = int.TryParse(konsoleNumberString, out konsoleNumber);
        }

        private static void HelpContent()
        {
            Console.WriteLine("--nc  => neue Konsole anlegen");
            Console.WriteLine("--ns  => neuen Speicher anlegen");
            Console.WriteLine("--ng  => neues Spiel anlegen");
            Console.WriteLine("--uk  => Konsole ändern");
            Console.WriteLine("--us  => Speicher ändern");
            Console.WriteLine("--ug  => Spiel ändern");
            Console.WriteLine("--dks => Konsole mit allen Speicherträgern löschen");
            Console.WriteLine("--dkc => Konsole mit allen Spielen löschen");
            Console.WriteLine("--dk  => Konsole löschen");
            Console.WriteLine("--ds  => Speicher löschen");
            Console.WriteLine("--dsc => Speicher mit allen Spielen löschen");
            Console.WriteLine("--dg  => Spiel löschenlöschen");
            Console.WriteLine("--save=> Speichen");
            Console.WriteLine("--ec  => Anwendung beenden");
        }

        private static void CreateConsole(IGameDataWorkflow dataManager)
        {
            string name = ConsoleAbfrage("Konsolenbezeichnung: ");
            var consoleName = ConsoleAbfrage("Benutzerdefinierter Name der Konsole: ");
            var spaceString = ConsoleAbfrage("Speicherplatz interner Speicher: ");

            var success = float.TryParse(spaceString, out float space);

            if (success)
                dataManager.CreateKonsole(consoleName, name, space);
        }

        private static string ConsoleAbfrage(string test)
        {
            Console.WriteLine(test);
            var name = Console.ReadLine().Replace(Environment.NewLine, "").Replace(" ", "");
            return name;
        }

        private static void FillWithNewData(IGameDataWorkflow dataManager)
        {
            var konsoleToAdd = dataManager.Konsolen.First();
            var kart128 = dataManager.CreateStorage(konsoleToAdd.Id, "Multiplayer Karte", 128);
            dataManager.CreateGame(konsoleToAdd.Storages.First(), "DOOM 2016", new List<Genre> { Genre.SingelPlayer, Genre.Egoshooter }, 20);
            dataManager.CreateGame(kart128.Id, "Mario Party", new List<Genre>() { Genre.Coop, Genre.Party }, 5);
        }

        private static void PrintAllData(IGameDataWorkflow dataManager)
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
