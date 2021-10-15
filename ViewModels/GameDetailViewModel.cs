using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class GameDetailViewModel : IDetailViewModel<Game>
    {
        private readonly IGameDataWorkflow workflow;
        public GameDetailViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}