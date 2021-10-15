using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class StoragesViewModel : IViewModel<Storage>
    {
        private readonly IGameDataWorkflow workflow;
        public StoragesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}