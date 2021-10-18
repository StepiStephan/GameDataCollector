using DataClasses;
using GameDataCollectorWorkflow.Contract;
using System.Collections.Generic;
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
            return workflow.CreateKonsole(konsoleName, name, internerSpeicher);
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