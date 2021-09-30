using DataClasses;
using Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging.Test
{
    public class DataManagerTest
    {
        DataManager dataManager;
        Konsole konsole;
        Storage newStorage;
        Storage intern;
        Game game;

        [SetUp]
        public void Setup()
        {
            dataManager = new DataManager();
            konsole = dataManager.CreateKonsole("XBoxX-alt", "XBox Sieres X", 500);
            intern = dataManager.GetStorage(konsole.Storages.First());
            newStorage = dataManager.CreateStorage(konsole.Id, "MultiplayerKarte", 1000);
            game = dataManager.CreateGame(konsole.Storages.First(), "DOOM", new List<Genre>() { Genre.Egoshooter }, 20);
        }

        [Test]
        public void EditTest()
        {

            Assert.Pass();
        }
        [Test]
        public void AddTest()
        {
            dataManager.AddGame(newStorage.Id, game);
            dataManager.AddStorage(konsole.Id, newStorage);

            if (!newStorage.Games.Contains(game.Id))
                Assert.Fail("Spiel nicht hinzugefügt");

            dataManager.DeleteStorage(newStorage.Id);

            if (!konsole.Storages.Contains(newStorage.Id))
                Assert.Fail("Speicher nicht hinzugefügt");

            Assert.Pass();
        }
        [Test]
        public void GetTest()
        {
            var getStorage = dataManager.GetStorage(intern.Id);
            var getKonsole = dataManager.GetKonsole(konsole.Id);
            var getGame = dataManager.GetGame(game.Id);

            if (intern != getStorage)
                Assert.Fail("Nicht gleicher Speicher");
            if (getKonsole != konsole)
                Assert.Fail("Nicht gleiche Konsole");
            if (getGame != game)
                Assert.Fail("Nicht gleiches Spiel");
            Assert.Pass();
            
        }
        [Test]
        public void AddDeleteKonsoleAllStoragesTest()
        {
            var resultGame = dataManager.GetGame(game.Id);
            var resultStorage = dataManager.GetStorage(newStorage.Id);

            if(resultStorage != newStorage)
            {
                Assert.Fail("Speicher wurde nicht angelegt");
            }

            if(konsole.Storages.Last() != newStorage.Id)
            {
                Assert.Fail("Karte wurde nicht mit konsole verknüpft");
            }

            if (resultGame != game)
            {
                Assert.Fail("Spiel wurde nicht hinzugefügt");
            }

            var resultKonsole = dataManager.Konsolen.Last();
            if(konsole != resultKonsole)
            {
                Assert.Fail("Konsole ist nicht hinzugefügt");
            }

            var storage = dataManager.GetStorage(konsole.Storages.First());

            if (storage == null)
            { 
                Assert.Fail("Interner Speicher wurde nicht hinzugefügt");
            }

            string storageID = konsole.Storages.First();
            dataManager.DeleteKonsoleWithAllStorages(konsole.Id);

            resultKonsole = dataManager.Konsolen.Last();

            if (konsole == resultKonsole)
            {
                Assert.Fail("Konsole ist nicht entfernt");
            }

            storage = dataManager.GetStorage(storageID);

            if (storage != null)
            {
                Assert.Fail("Interner Speicher wurde entfernt");
            }

            resultGame = dataManager.GetGame(game.Id);

            if(resultGame != game)
            {
                Assert.Fail("Spiel wurde gelöscht");
            }

            resultStorage = dataManager.GetStorage(newStorage.Id);

            if(resultStorage == newStorage)
            {
                Assert.Fail("Speicher wurde nicht gelöscht");
            }

            dataManager.DeleteGame(game.Id);
            Assert.Pass();
        }
        [Test]
        public void AddDeleteKonsoleAllSGamesTest()
        {
            var konsole = dataManager.CreateKonsole("XBoxX-alt", "XBox Sieres X", 500);
            var newStorage = dataManager.CreateStorage(konsole.Id, "MultiplayerKarte", 1000);
            var game = dataManager.CreateGame(konsole.Storages.First(), "DOOM", new List<Genre>() { Genre.Egoshooter }, 20);
            var resultGame = dataManager.GetGame(game.Id);
            var resultStorage = dataManager.GetStorage(newStorage.Id);

            if (resultStorage != newStorage)
            {
                Assert.Fail("Speicher wurde nicht angelegt");
            }

            if (konsole.Storages.Last() != newStorage.Id)
            {
                Assert.Fail("Karte wurde nicht mit konsole verknüpft");
            }

            if (resultGame != game)
            {
                Assert.Fail("Spiel wurde nicht hinzugefügt");
            }

            var resultKonsole = dataManager.Konsolen.Last();
            if (konsole != resultKonsole)
            {
                Assert.Fail("Konsole ist nicht hinzugefügt");
            }

            var storage = dataManager.GetStorage(konsole.Storages.First());

            if (storage == null)
            {
                Assert.Fail("Interner Speicher wurde nicht hinzugefügt");
            }

            string storageID = konsole.Storages.First();
            dataManager.DeleteKonsoleWithAllGames(konsole.Id);

            resultKonsole = dataManager.Konsolen.Last();

            if (konsole == resultKonsole)
            {
                Assert.Fail("Konsole ist nicht entfernt");
            }

            storage = dataManager.GetStorage(storageID);

            if (storage != null)
            {
                Assert.Fail("Interner Speicher wurde entfernt");
            }

            resultStorage = dataManager.GetStorage(newStorage.Id);

            if (resultStorage == newStorage)
            {
                Assert.Fail("Speicher wurde nicht gelöscht");
            }

            resultGame = dataManager.GetGame(game.Id);

            if (resultGame == game)
            {
                Assert.Fail("Spiel wurde nicht gelöscht");
            }

            Assert.Pass();
        }
        [Test]
        public void AddDeleteKonsole()
        {
            var konsole = dataManager.CreateKonsole("XBoxX-alt", "XBox Sieres X", 500);
            var newStorage = dataManager.CreateStorage(konsole.Id, "MultiplayerKarte", 1000);
            var game = dataManager.CreateGame(konsole.Storages.First(), "DOOM", new List<Genre>() { Genre.Egoshooter }, 20);
            var resultGame = dataManager.GetGame(game.Id);
            var resultStorage = dataManager.GetStorage(newStorage.Id);

            if (resultStorage != newStorage)
            {
                Assert.Fail("Speicher wurde nicht angelegt");
            }

            if (konsole.Storages.Last() != newStorage.Id)
            {
                Assert.Fail("Karte wurde nicht mit konsole verknüpft");
            }

            if (resultGame != game)
            {
                Assert.Fail("Spiel wurde nicht hinzugefügt");
            }

            var resultKonsole = dataManager.Konsolen.Last();
            if (konsole != resultKonsole)
            {
                Assert.Fail("Konsole ist nicht hinzugefügt");
            }

            var storage = dataManager.GetStorage(konsole.Storages.First());

            if (storage == null)
            {
                Assert.Fail("Interner Speicher wurde nicht hinzugefügt");
            }

            string storageID = konsole.Storages.First();
            dataManager.DeleteKonsole(konsole.Id);

            resultKonsole = dataManager.Konsolen.Last();

            if (konsole == resultKonsole)
            {
                Assert.Fail("Konsole ist nicht entfernt");
            }

            storage = dataManager.GetStorage(storageID);

            if (storage != null)
            {
                Assert.Fail("Interner Speicher wurde nicht entfernt");
            }

            resultStorage = dataManager.GetStorage(newStorage.Id);

            if (resultStorage != newStorage)
            {
                Assert.Fail("Speicher wurde gelöscht");
            }

            resultGame = dataManager.GetGame(game.Id);

            if (resultGame == game)
            {
                Assert.Fail("Spiel wurde gelöscht");
            }

            dataManager.DeleteStorage(newStorage.Id);

            Assert.Pass();
        }
    }
}
