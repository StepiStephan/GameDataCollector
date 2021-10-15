using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class GamesViewModel : IViewModel<Game>
    {
        private readonly IGameDataWorkflow workflow;
        public GamesViewModel(IGameDataWorkflow workflow)
        {
            this.workflow = workflow;
        }
    }
}