using DataClasses;
using DataManaging;
using DataManaging.Contract;
using GameDataCollectorWorkflow;
using GameDataCollectorWorkflow.Contract;
using Ninject;
using System.Collections.Generic;

namespace Infrastructure
{
    public class DIKernal
    {
        public void SetDI(StandardKernel kernel)
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

        public void SetXamarinDI()
        {
            Xamarin.Forms.DependencyService.Register<IDataLoader<List<Game>> ,DataLoader <List<Game>>>();
            Xamarin.Forms.DependencyService.Register<IDataLoader<List<Konsole>> ,DataLoader <List<Konsole>>>();
            Xamarin.Forms.DependencyService.Register<IDataLoader<List<Storage>>, DataLoader<List<Storage>>>();
            Xamarin.Forms.DependencyService.Register<IDataSaver<List<Game>>, DataSaver<List<Game>>>();
            Xamarin.Forms.DependencyService.Register<IDataSaver<List<Konsole>>, DataSaver<List<Konsole>>>();
            Xamarin.Forms.DependencyService.Register<IDataSaver<List<Storage>>, DataSaver<List<Storage>>>();
            Xamarin.Forms.DependencyService.Register<IGameManager, GameManager>();
            Xamarin.Forms.DependencyService.Register<IKonsoleManager, KonsoleManager>();
            Xamarin.Forms.DependencyService.Register<IStorageManager, StorageManager>();
            Xamarin.Forms.DependencyService.Register<IGameDataWorkflow, GameDataWorkflow>();
        }
    }
}
