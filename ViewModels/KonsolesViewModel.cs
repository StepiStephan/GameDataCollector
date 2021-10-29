using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System.Collections.Generic;
using System.Linq;
using ViewModels.Contract;

namespace ViewModels
{
    public class KonsolesViewModel : IKonsoleViewModel
    {
        private readonly IGameDataWorkflow workflow;

        public KonsolesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }

        public List<Konsole> Konsoles => workflow.Konsolen;

        public void AddStorage(string konsoleId, Storage storage)
        {
            workflow.AddStorage(konsoleId,storage);
        }

        public Konsole CreateKonsole(string konsoleName, string name, float internerSpeicher)
        {
            var konsole = workflow.CreateKonsole(konsoleName, name, internerSpeicher);
            return konsole;
        }

        public void DeleteKonsole(string id)
        {
            workflow.DeleteKonsole(id);
        }

        public void DeleteKonsoleWithAllGames(string id)
        {
            workflow.DeleteKonsoleWithAllGames(id);
        }

        public void DeleteKonsoleWithAllStorages(string id)
        {
            workflow.DeleteKonsoleWithAllStorages(id);
        }

        public void EditKonsole(string konsoleId, string name, string consoleName)
        {
            workflow.EditKonsole(konsoleId, name, consoleName);
        }

        public string GetInfo(Konsole konsole)
        {
            int count = 0;
            var storages = workflow.GetStorages().Where(x => konsole.Storages.Contains(x.Id));
            foreach(var storage in storages)
            {
                count += storage.Games.Count();
            }
            return count.ToString();
        }

        public Konsole GetKonsole(string konsoleId)
        {
            return workflow.GetKonsole(konsoleId);
        }

        public void SetKonsole(string id)
        {
            workflow.SelectKonsole(id);
        }
    }
}