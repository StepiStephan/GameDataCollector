using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class KonsoleDetailViewModel : IDetailViewModel<Konsole>
    {
        private readonly IGameDataWorkflow workflow;

        public KonsoleDetailViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}