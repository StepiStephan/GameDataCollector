using Enums;
using GameDataCollectorWorkflow.Contract;
using Infrastructure;
using Ninject;
using System;
using System.Collections.Generic;

namespace TestKonsole
{
    class Program
    {
        const int countOfGenres = 23;
        static void Main()
        {
            StandardKernel kernel = new StandardKernel();
            DIKernal.SetDI(kernel);
            IGameDataWorkflow dataManager = kernel.Get<IGameDataWorkflow>();

            RunConsole(dataManager);
            Console.Read();
        }

        private static void RunConsole(IGameDataWorkflow dataManager)
        {
            bool run = true;
            while (run)
            {
                Console.WriteLine("Komando eingeben: ");
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
                        EditGame(dataManager);
                        break;

                    case "--dkc":
                        DeleteCompleteKonsole(dataManager);
                        break;
                    case "--dks":
                        DeleteKonsoleStorage(dataManager);
                        break;

                    case "--dk":
                        DeleteKonsole(dataManager);
                        break;

                    case "--ds":
                        DeleteStorage(dataManager);
                        break;
                    case "--dsc":
                        DeleteCompleteStorage(dataManager);
                        break;

                    case "--dg":
                        DeleteGame(dataManager);
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

        private static void DeleteGame(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    GetGameNumber(dataManager, dataManager.Storages[storageNumber].Id, out bool gameSuccess, out int gameNumber);
                    if (gameSuccess)
                    {
                        dataManager.DeleteGame(dataManager.Games[gameNumber].Id);
                    }
                }
            }
        }

        private static void DeleteCompleteStorage(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    dataManager.DeleteStorageWithGames(dataManager.Storages[storageNumber].Id);
                }
            }
        }

        private static void DeleteStorage(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    dataManager.DeleteStorage(dataManager.Storages[storageNumber].Id);
                }
            }
        }

        private static void DeleteKonsole(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool konsoleSuccess, out int konsoleNumber);
            if (konsoleSuccess)
            {
                dataManager.DeleteKonsole(dataManager.Konsolen[konsoleNumber].Id);
            }
        }

        private static void DeleteKonsoleStorage(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool konsoleSuccess, out int konsoleNumber);
            if (konsoleSuccess)
            {
                dataManager.DeleteKonsoleWithAllStorages(dataManager.Konsolen[konsoleNumber].Id);
            }
        }

        private static void DeleteCompleteKonsole(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool konsoleSuccess, out int konsoleNumber);
            if (konsoleSuccess)
            {
                dataManager.DeleteKonsoleWithAllGames(dataManager.Konsolen[konsoleNumber].Id);
            }
        }

        private static void EditGame(IGameDataWorkflow dataManager)
        {
            GetKonsoleNumber(dataManager, out bool success, out int konsoleNumber);
            if (success)
            {
                GetStorageNumber(dataManager, dataManager.Konsolen[konsoleNumber].Id, out bool storageSuccess, out int storageNumber);
                if (storageSuccess)
                {
                    GetGameNumber(dataManager, dataManager.Storages[storageNumber].Id, out bool gameSuccess, out int gameNumber);
                    if (gameSuccess)
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
                        dataManager.EditGame(dataManager.Games[gameNumber].Id, name, space);
                    }
                }
            }
        }

        private static void GetGameNumber(IGameDataWorkflow dataManager, string storageId, out bool gameSuccess, out int gameNumber)
        {
            var storage = dataManager.GetStorage(storageId);

            for (int i = 0; i < dataManager.Games.Count; i++)
            {
                var game = dataManager.Games[i];
                if (storage.Games.Contains(game.Id))
                {
                    Console.WriteLine($"[{i}] => GameName: {game.Name}");
                }
            }
            var gameNumberString = ConsoleAbfrage("Wähle ein Spiel: ");
            gameSuccess = int.TryParse(gameNumberString, out gameNumber);
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
            for(int i = 0; i < countOfGenres; i++)
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
                string spaceString = ConsoleAbfrage("Speicherplatz: ");
                var succsessSpace = float.TryParse(spaceString, out float space);
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

        private static void GetStorageNumber(IGameDataWorkflow dataManager, string konsoleID, out bool succsess, out int storageNumber)
        {
            var konsole = dataManager.GetKonsole(konsoleID);

            for (int i = 0; i < dataManager.Storages.Count; i++)
            {
                var storage = dataManager.Storages[i];
                if (konsole.Storages.Contains(storage.Id))
                {
                    Console.WriteLine($"[{i}] => StorageName: {storage.Name}");
                }
            }
            var konsoleNumberString = ConsoleAbfrage("Wähle ein Speicher: ");
            succsess = int.TryParse(konsoleNumberString, out storageNumber);
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
            Console.WriteLine("--pd  => Daten anzeigen");
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
            var name = Console.ReadLine().Replace(Environment.NewLine, "");
            return name;
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
