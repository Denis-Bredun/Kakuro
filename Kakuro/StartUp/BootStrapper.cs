using Autofac;
using Kakuro.Commands;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Enums;
using Kakuro.Game_Tools;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Interfaces.Data_Access.Tools;
using Kakuro.Interfaces.Game_Tools;
using Kakuro.Models;
using Kakuro.ViewModels;

namespace Kakuro.StartUp
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();

            builder.Register(c => new DashboardItemCollection())
                   .AsSelf()
                   .SingleInstance();

            builder.RegisterType<EventAggregator>().As<IEventAggregator>().SingleInstance();

            // ViewModels
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<DashboardItemViewModel>().AsSelf();
            builder.RegisterType<DashboardViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<RatingTableViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<SettingsViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<SavepointsViewModel>().AsSelf().SingleInstance();

            // Commands
            builder.RegisterType<CleanDashboardCommand>().AsSelf().SingleInstance();
            builder.RegisterType<ValidateSolutionCommand>().AsSelf().SingleInstance();

            // Data Access
            builder.RegisterType<DashboardProvider>().As<IDashboardProvider>().SingleInstance();
            builder.RegisterType<DashboardTemplateProvider>().As<IDashboardTemplateProvider>().SingleInstance();
            builder.RegisterType<RatingRecordProvider>().As<IRatingRecordProvider>().SingleInstance();
            builder.RegisterType<SavepointProvider>().As<ISavepointProvider>().SingleInstance();
            builder.RegisterType<RatingRecordRepository>().As<IReadAllRepository<RatingRecord, DifficultyLevels>>().SingleInstance();
            builder.RegisterType<SavepointRepository>().As<IRepository<Savepoint>>().SingleInstance();
            builder.RegisterType<SavepointProvider>().As<ISavepointProvider>().SingleInstance();
            builder.RegisterType<JsonFileHandler<RatingRecord>>().As<IJsonFileHandler<RatingRecord>>().SingleInstance();
            builder.RegisterType<JsonFileHandler<Savepoint>>().As<IJsonFileHandler<Savepoint>>().SingleInstance();

            // Game Tools:
            builder.RegisterType<SolutionVerifier>().As<ISolutionVerifier>().SingleInstance();
            builder.RegisterType<OperationNotifier>().As<IOperationNotifier>().SingleInstance();


            return builder.Build();
        }
    }
}
