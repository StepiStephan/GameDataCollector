using DataClasses;
using GameDataCollectorWorkflow.Contract;
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
    }
}