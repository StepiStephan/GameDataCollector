using GameDataCollectorWorkflow.Contract;
using System;
using System.Collections.Generic;
using System.Text;
using ViewModels.Contract;

namespace ViewModels
{
    public class StartPageViewModel : IStartPageViewModel
    {
        IGameDataWorkflow workflow;
        public StartPageViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
        public int GetGameCount()
        {
            return workflow.Games.Count;
        }

        public int GetKonsolenCount()
        {
            return workflow.Konsolen.Count;
        }

        public int GetStorageCount()
        {
            return workflow.Storages.Count;
        }
    }
}
