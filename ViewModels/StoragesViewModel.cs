using DataClasses;
using GameDataCollectorWorkflow.Contract;
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
    }
}