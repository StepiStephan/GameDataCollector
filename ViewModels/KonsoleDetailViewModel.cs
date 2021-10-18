using DataClasses;
using GameDataCollectorWorkflow.Contract;
using ViewModels.Contract;

namespace ViewModels
{
    public class KonsoleDetailViewModel :KonsolesViewModel, IDetailViewModel<Konsole>
    {
        public KonsoleDetailViewModel(IGameDataWorkflow workflow) : base(workflow)
        {
        }
    }
}