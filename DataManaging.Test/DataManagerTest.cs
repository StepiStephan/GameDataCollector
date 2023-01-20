using CsvTeDataManagingst;
using DataClasses;
using Enums;
using GameDataCollectorWorkflow;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging.Test
{
    public class DataManagerTest
    {
        GameDataCollectorWorkflow.GameDataWorkflow dataManager;
        GameManager gm;
        KonsoleManager km;
        StorageManager sm;
        Konsole konsole;
        Storage newStorage;
        Storage intern;
        Game game;
        CsvParser csvParser;

        [SetUp]
        public void Setup()
        {
            gm = new GameManager(new DataSaver<List<Game>>(), new DataLoader<List<Game>>());
            sm = new StorageManager(new DataSaver<List<Storage>>(), new DataLoader<List<Storage>>());
            km = new KonsoleManager(new DataSaver<List<Konsole>>(), new DataLoader<List<Konsole>>());
            csvParser= new CsvParser();
            dataManager = new GameDataCollectorWorkflow.GameDataWorkflow(gm, sm, km);

            konsole = dataManager.CreateKonsole("XBoxX-alt", "XBox Sieres X", 500);
            intern = dataManager.GetStorage(konsole.Storages.First());
            newStorage = dataManager.CreateStorage(konsole.Id, "MultiplayerKarte", 1000);
            game = dataManager.CreateGame(konsole.Storages.First(), "DOOM", new List<Genre>() { Genre.Egoshooter }, 20);
        }

        [Test]
        public void CopyElementTest()
        {
            var editGame = gm.Copy(game);

            if (editGame.Name != game.Name)
                Assert.Fail("GameName Falsch");

            if(editGame.Id != game.Id)
                Assert.Fail("GameID Falsch");

            if (editGame.SpaceOnSorage != game.SpaceOnSorage)
                Assert.Fail("GameSpace Falsch");

            var editStorage = sm.Copy(newStorage.Id);

            if (editStorage.Id != newStorage.Id)
                Assert.Fail("newStorage.Id Falsch");

            if (editStorage.Name != newStorage.Name)
                Assert.Fail("newStorage.Name Falsch");

            if (editStorage.Space != newStorage.Space)
                Assert.Fail("newStorage.Space Falsch");

            if (editStorage.Games.Count != newStorage.Games.Count)
                Assert.Fail("newStorage.Games Falsch");

            var editKonsole = km.Copy(konsole.Id);

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
            var editGame = gm.Copy(game);
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

            var editStorage = sm.Copy(newStorage.Id);
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

            var editKonsole = km.Copy(konsole.Id);
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

        [Test]
        public void CsvParserTest()
        {
            var tableName = "Playstation 5";
            var tableHeader = "Name;Erscheinungsdatum;Preis(NetGames);Preis(Medimops);Preis(Rebuy)";
            var game1 = "God Of War;18.10.2022;50;40;30";
            var game2 = "A Plaque Tale Requiem;2.4.2023;60;;;";

            string[] testCase = new string[] 
            {
                tableName,
                tableHeader,
                game1,
                game2
            };
            TableClass tableClass = new TableClass();
            csvParser.ParseLines(tableClass, testCase);

            Assert.AreEqual(tableClass.TableName, tableName);
            Assert.AreEqual(tableClass.ColumnCaptions[0], "Name");
            Assert.AreEqual(tableClass.ColumnCaptions[1], "Erscheinungsdatum");
            Assert.AreEqual(tableClass.ColumnCaptions[2], "NetGames");
            Assert.AreEqual(tableClass.ColumnCaptions[3], "Medimops");
            Assert.AreEqual(tableClass.ColumnCaptions[4], "Rebuy");
            Assert.AreEqual(tableClass.Content.Names[0], "God Of War");
            Assert.AreEqual(tableClass.Content.Names[1], "A Plaque Tale Requiem");
            Assert.AreEqual(tableClass.Content.Günstigster[0], 30f);
            Assert.AreEqual(tableClass.Content.Günstigster[1], 60f);
            Assert.AreEqual(tableClass.Content.ReleaseDate[0], new DateTime(2022, 10, 18));
            Assert.AreEqual(tableClass.Content.ReleaseDate[1], new DateTime(2023, 4, 2));
        }
    }
}
