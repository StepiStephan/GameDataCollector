using DataClasses;
using DataManaging;
using DataManaging.Contract;
using GameDataCollectorWorkflow;
using GameDataCollectorWorkflow.Contract;
using Microsoft.Extensions.DependencyInjection;
using Ninject;
using System.Collections.Generic;
using ViewModels;
using ViewModels.Contract;

namespace Infrastructure
{
    public static class DIKernal
    {
        public static ServiceProvider SetDIMicrosoft(IServiceCollection services)
        {
            services.AddSingleton<IDataLoader<List<Game>>, DataLoader<List<Game>>>();
            services.AddSingleton<IDataLoader<List<Konsole>>, DataLoader<List<Konsole>>>();
            services.AddSingleton<IDataLoader<List<Storage>>, DataLoader<List<Storage>>>();
            services.AddSingleton<IDataSaver<List<Game>>,DataSaver <List<Game>>>();
            services.AddSingleton<IDataSaver<List<Konsole>>, DataSaver<List<Konsole>>>();
            services.AddSingleton<IDataSaver<List<Storage>>, DataSaver<List<Storage>>>();
            services.AddSingleton<IGameManager, GameManager>();
            services.AddSingleton<IKonsoleManager, KonsoleManager>();
            services.AddSingleton<IStorageManager, StorageManager>();
            services.AddSingleton<IGameDataWorkflow, GameDataWorkflow>();
            services.AddSingleton<IDetailViewModel<Game>, GameDetailViewModel>();
            services.AddSingleton<IDetailViewModel<Storage>, StorageDetailViewModel>();
            services.AddSingleton<IDetailViewModel<Konsole>, KonsoleDetailViewModel>();
            services.AddSingleton<IGameViewModel, GamesViewModel>();
            services.AddSingleton<IStorageViewModel, StoragesViewModel>();
            services.AddSingleton<IKonsoleViewModel, KonsolesViewModel>();
            services.AddSingleton<IStatisticViewModel, StatisticViewModel>();
            services.AddSingleton<ISearchViewModel, SearchViewModel>();
            services.AddSingleton<IStartPageViewModel, StartPageViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
