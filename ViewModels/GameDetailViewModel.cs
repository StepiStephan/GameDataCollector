using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class GameDetailViewModel : GamesViewModel, IDetailViewModel<Game>
    {
        public GameDetailViewModel(IGameDataWorkflow workflow) : base(workflow)
        {
        }

        public string ItemId { get; set; }
    }
}