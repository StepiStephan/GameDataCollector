using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class KonsolesViewModel : IViewModel<Konsole>
    {
        private readonly IGameDataWorkflow workflow;

        public KonsolesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}