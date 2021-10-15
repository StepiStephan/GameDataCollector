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
        public static void SetDI(IKernel kernel)
        {
            kernel.Bind<IDataLoader<List<Game>>>().To<DataLoader<List<Game>>>() ;
            kernel.Bind<IDataLoader<List<Konsole>>>().To<DataLoader<List<Konsole>>>();
            kernel.Bind<IDataLoader<List<Storage>>>().To<DataLoader<List<Storage>>>();
            kernel.Bind<IDataSaver<List<Game>>>().To<DataSaver<List<Game>>>();
            kernel.Bind<IDataSaver<List<Konsole>>>().To<DataSaver<List<Konsole>>>();
            kernel.Bind<IDataSaver<List<Storage>>>().To<DataSaver<List<Storage>>>();
            kernel.Bind<IGameManager>().To<GameManager>();
            kernel.Bind<IKonsoleManager>().To<KonsoleManager>();
            kernel.Bind<IStorageManager>().To<StorageManager>();
            kernel.Bind<IGameDataWorkflow>().To<GameDataWorkflow>();
        }

        //public static void SetXamarinDI()
        //{
        //    Xamarin.Forms.DependencyService.Register<IDataLoader<List<Game>> ,DataLoader <List<Game>>>();
        //    Xamarin.Forms.DependencyService.Register<IDataLoader<List<Konsole>> ,DataLoader <List<Konsole>>>();
        //    Xamarin.Forms.DependencyService.Register<IDataLoader<List<Storage>>, DataLoader<List<Storage>>>();
        //    Xamarin.Forms.DependencyService.Register<IDataSaver<List<Game>>, DataSaver<List<Game>>>();
        //    Xamarin.Forms.DependencyService.Register<IDataSaver<List<Konsole>>, DataSaver<List<Konsole>>>();
        //    Xamarin.Forms.DependencyService.Register<IDataSaver<List<Storage>>, DataSaver<List<Storage>>>();
        //    Xamarin.Forms.DependencyService.Register<IGameManager, GameManager>();
        //    Xamarin.Forms.DependencyService.Register<IKonsoleManager, KonsoleManager>();
        //    Xamarin.Forms.DependencyService.Register<IStorageManager, StorageManager>();
        //    Xamarin.Forms.DependencyService.Register<IGameDataWorkflow, GameDataWorkflow>();
        //}

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

            return services.BuildServiceProvider();
        }
    }
}
