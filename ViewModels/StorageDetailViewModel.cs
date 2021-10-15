using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class StorageDetailViewModel : IDetailViewModel<Storage>
    {
        private readonly IGameDataWorkflow workflow;

        public StorageDetailViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}