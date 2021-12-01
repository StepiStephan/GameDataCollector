using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Contract;

namespace ViewModels
{
    public class StoragesViewModel : IStorageViewModel
    {
        private readonly IGameDataWorkflow workflow;
        public StoragesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
        public Konsole SelectedKonsole { get => workflow.GetKonsole(workflow.SelectedKonsole); set => workflow.SelectKonsole(value.Id); }

        public List<Storage> Storages => workflow.Storages;

        public List<Konsole> Konsolen => workflow.Konsolen;

        public void AddGame(string storageId, Game game)
        {
            workflow.AddGame(storageId, game);
        }

        public Storage CreateStorage(string konsoleId, string name, float space)
        {
            return workflow.CreateStorage(konsoleId, name, space);
        }

        public void DeleteStorage(string id)
        {
            workflow.DeleteStorage(id);
        }

        public void DeleteStorageWithGames(string id)
        {
            workflow.DeleteStorageWithGames(id);
        }

        public void EditStorage(string storageId, string name, float space)
        {
            workflow.EditStorage(storageId,name,space);
        }

        public string GetInfos(Storage storage)
        {
            var konsole = workflow.Konsolen.Where(x => x.Storages.Contains(storage.Id)).FirstOrDefault();
            if(konsole == null)
            {
                return "";
            }
            return konsole.Name;
        }

        public Storage GetStorage(string storageId)
        {
            return workflow.GetStorage(storageId);
        }

        public List<Storage> GetStorages()
        {
            return workflow.GetStorages();
        }

        public void MoveStorage(string oldKonsoleId, string storageId, string newKonsoleId)
        {
            workflow.MoveStorage(oldKonsoleId, storageId, newKonsoleId);
        }

        public void SetStorage(string id)
        {
            workflow.SelectStorage(id);
        }
    }
}