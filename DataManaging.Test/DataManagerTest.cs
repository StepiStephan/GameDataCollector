using DataClasses;
using Enums;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging.Test
{
    public class DataManagerTest
    {
        GameDataCollectorWorkflow.GameDataWorkflow dataManager;
        Konsole konsole;
        Storage newStorage;
        Storage intern;
        Game game;

        [SetUp]
        public void Setup()
        {
            dataManager = new GameDataCollectorWorkflow.GameDataWorkflow(
                new GameManager(
                    new DataSaver<List<Game>>(), new DataLoader<List<Game>>()),
                new StorageManager(
                    new DataSaver<List<Storage>>(), new DataLoader<List<Storage>>()),
                new KonsoleManager(
                    new DataSaver<List<Konsole>>(), new DataLoader<List<Konsole>>()))                ;
            konsole = dataManager.CreateKonsole("XBoxX-alt", "XBox Sieres X", 500);
            intern = dataManager.GetStorage(konsole.Storages.First());
            newStorage = dataManager.CreateStorage(konsole.Id, "MultiplayerKarte", 1000);
            game = dataManager.CreateGame(konsole.Storages.First(), "DOOM", new List<Genre>() { Genre.Egoshooter }, 20);
        }

        [Test]
        public void CopyElementTest()
        {
            var editGame = game.Copy();

            if (editGame.Name != game.Name)
                Assert.Fail("GameName Falsch");

            if(editGame.Id != game.Id)
                Assert.Fail("GameID Falsch");

            if (editGame.SpaceOnSorage != game.SpaceOnSorage)
                Assert.Fail("GameSpace Falsch");

            var editStorage = newStorage.Copy();

            if (editStorage.Id != newStorage.Id)
                Assert.Fail("newStorage.Id Falsch");

            if (editStorage.Name != newStorage.Name)
                Assert.Fail("newStorage.Name Falsch");

            if (editStorage.Space != newStorage.Space)
                Assert.Fail("newStorage.Space Falsch");

            if (editStorage.Games.Count != newStorage.Games.Count)
                Assert.Fail("newStorage.Games Falsch");

            var editKonsole = konsole.Copy();

            if (editKonsole.Id != konsole.Id)
                Assert.Fail("konsole.Id Falsch");

            if (editKonsole.Name != konsole.Name)
                Assert.Fail("konsole.Name Falsch");

            if (editKonsole.Storages.Count != konsole.Storages.Count)
                Assert.Fail("konsole.Storages Falsch");

            if (editKonsole.ConsoleName != konsole.ConsoleName)
                Assert.Fail("konsole.ConsoleName Falsch");
        }
        [Test]
        public void EditTest()
        {
            var editGame = game.Copy();
            dataManager.EditGame(game.Id, null, 200);
            if(dataManager.GetGame(game.Id).SpaceOnSorage != 200)
                Assert.Fail("Gamespace nicht editiert");

            if (dataManager.GetGame(game.Id).Name != editGame.Name)
                Assert.Fail("GameName wurde editiert");

            dataManager.EditGame(game.Id, "Dishonored", 0);

            if(dataManager.GetGame(game.Id).Name != "Dishonored")
                Assert.Fail("Gamename nicht editiert");

            if (dataManager.GetGame(game.Id).SpaceOnSorage != 200)
                Assert.Fail("Gamespace wurde editiert");

            dataManager.EditGame(game.Id, editGame.Name, editGame.SpaceOnSorage);

            var editStorage = newStorage.Copy();
            dataManager.EditStorage(newStorage.Id, "TestName", 0);

            if (dataManager.GetStorage(newStorage.Id).Name != "TestName")
                Assert.Fail("Storagename wurde nicht geändert");

            if (dataManager.GetStorage(newStorage.Id).Space != editStorage.Space)
                Assert.Fail("Storagespace wurde nicht geändert");

            dataManager.EditStorage(newStorage.Id, null, 632);

            if (dataManager.GetStorage(newStorage.Id).Space != 632)
                Assert.Fail("Storagespace wurde nicht geändert");

            if (dataManager.GetStorage(newStorage.Id).Name != "TestName")
                Assert.Fail("Storagename wurde geändert");

            dataManager.EditStorage(newStorage.Id, editStorage.Name, editStorage.Space);

            var editKonsole = konsole.Copy();
            dataManager.EditKonsole(konsole.Id, "TestXBox", null);

            if (dataManager.GetKonsole(konsole.Id).Name == editKonsole.Name)
                Assert.Fail("Konsolename wurde nicht geändert");

            if (dataManager.GetKonsole(konsole.Id).ConsoleName != editKonsole.ConsoleName)
                Assert.Fail("ConsoleName wurde geändert");

            dataManager.EditKonsole(konsole.Id, null, "MyTestXBox");

            if (dataManager.GetKonsole(konsole.Id).Name != "TestXBox")
                Assert.Fail("Konsolename wurde geändert");

            if (dataManager.GetKonsole(konsole.Id).ConsoleName != "MyTestXBox")
                Assert.Fail("ConsoleName wurde nicht geändert");

            dataManager.EditKonsole(newStorage.Id, editKonsole.ConsoleName, editKonsole.Name);

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
