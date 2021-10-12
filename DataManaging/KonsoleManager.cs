using DataClasses;
using DataManaging.Contract;
using System.Collections.Generic;
using System.Linq;

namespace DataManaging
{
    public class KonsoleManager : IKonsoleManager
    {
        private readonly List<Konsole> konsoles;
        private const string konsoleName = "KonsoleList";
        private readonly IDataSaver<List<Konsole>> dataSaverKonsole;
        private readonly IDataLoader<List<Konsole>> dataLoaderKonsole;

        public IEnumerable<Konsole> Konsoles { get => konsoles; }

        public KonsoleManager(IDataSaver<List<Konsole>> dataSaverKonsole, IDataLoader<List<Konsole>> dataLoaderKonsole)
        {
            this.dataLoaderKonsole = dataLoaderKonsole;
            this.dataSaverKonsole = dataSaverKonsole;
            this.dataSaverKonsole.SetName(konsoleName);
            this.dataLoaderKonsole.SetName(konsoleName);
            konsoles = dataLoaderKonsole.LoadObject();
            if (konsoles == null)
            {
                konsoles = new List<Konsole>();
                dataSaverKonsole.SaveObject(konsoles);
            }
        }
        public void SaveData()
        {
            dataSaverKonsole.SaveObject(konsoles);
        }

        public Konsole GetKonsole(string konsoleId)
        {
            return konsoles.Where(x => x.Id == konsoleId).FirstOrDefault();
        }
        public void AddKonsole(Konsole konsole)
        {
            konsoles.Add(konsole);
        }
        public void EditKonsole(string konsoleId, string consoleName, string name)
        {
            var konsole = konsoles.Where(x => x.Id == konsoleId).FirstOrDefault();
            if (konsole != null)
            {
                if (name == null || name == string.Empty)
                {
                    name = konsole.Name;
                }
                if (consoleName == null || consoleName == string.Empty)
                {
                    consoleName = konsole.ConsoleName;
                }

                var newKonsole = new Konsole(consoleName, name, konsole.Id);
                konsoles.Remove(konsole);
                konsoles.Add(newKonsole);
            }
        }
        public void DeleteKonsoleWithAllStorages(string id, IStorageManager storageManager)
        {
            var konsole = GetKonsole(id);
            if (konsole != null)
            {
                List<string> storageIds = new List<string>();

                foreach (var storage in konsole.Storages)
                {
                    storageIds.Add(storage);
                }

                foreach (var storage in storageIds)
                {
                    storageManager.DeleteStorage(storage);
                }

                konsoles.Remove(konsole);
            }
        }
        public void DeleteKonsole(string id, IStorageManager storageManager, IGameManager gameManager)
        {
            var konsole = GetKonsole(id);

            if (konsole != null)
            {
                storageManager.DeleteStorageWithGames(konsole.Storages.First(), gameManager);
                konsoles.Remove(konsole);
            }
        }
        public void DeleteKonsoleWithAllGames(string id, IStorageManager storageManager, IGameManager gameManager)
        {
            var konsole = GetKonsole(id);
            if (konsole != null)
            {
                List<string> storageIds = new List<string>();

                foreach (var storage in konsole.Storages)
                {
                    storageIds.Add(storage);
                }

                foreach (var storage in storageIds)
                {
                    storageManager.DeleteStorageWithGames(storage, gameManager);
                }

                konsoles.Remove(konsole);
            }
        }
    }
}