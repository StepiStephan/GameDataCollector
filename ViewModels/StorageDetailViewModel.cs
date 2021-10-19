using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class StorageDetailViewModel :StoragesViewModel, IDetailViewModel<Storage>
    {
        public StorageDetailViewModel(IGameDataWorkflow workflow) : base(workflow)
        {
        }

        public string ItemId { get; set; }
    }
}