using Autofac;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Repositories;
using Kakuro.Data_Access.Tools;
using Kakuro.Enums;
using Kakuro.Game_Tools;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Repositories;
using Kakuro.Interfaces.Data_Access.Tools;
using Kakuro.Interfaces.Game_Tools;
using Kakuro.Interfaces.ViewModels;
using Kakuro.Models;
using Kakuro.ViewModels;

namespace Kakuro.StartUp
{
    public class BootStrapper
    {
        public IContainer BootStrap()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<MainWindow>().AsSelf();

            builder.Register(c => new DashboardItemCollection())
                   .AsSelf()
                   .SingleInstance();

            // ViewModels
            builder.RegisterType<MainViewModel>().AsSelf();
            builder.RegisterType<DashboardItemViewModel>().As<IDashboardItemViewModel>();
            builder.RegisterType<DashboardViewModel>()
                   .As<IDashboardViewModel>()
                   .WithParameter((pi, ctx) => pi.ParameterType == typeof(IDashboardProvider),
                                  (pi, ctx) => ctx.Resolve<IDashboardProvider>())
                   .WithParameter((pi, ctx) => pi.ParameterType == typeof(ISolutionVerifier),
                                  (pi, ctx) => ctx.Resolve<ISolutionVerifier>())
                   .WithParameter((pi, ctx) => pi.ParameterType == typeof(DashboardItemCollection),
                                  (pi, ctx) => ctx.Resolve<DashboardItemCollection>());


            // Data Access
            builder.RegisterType<DashboardProvider>()
                   .As<IDashboardProvider>()
                   .WithParameter((pi, ctx) => pi.ParameterType == typeof(DashboardItemCollection),
                                  (pi, ctx) => ctx.Resolve<DashboardItemCollection>());
            builder.RegisterType<DashboardTemplateProvider>().As<IDashboardTemplateProvider>();
            builder.RegisterType<RatingRecordProvider>().As<RatingRecordProvider>();
            builder.RegisterType<SavepointProvider>().As<ISavepointProvider>();
            builder.RegisterType<RatingRecordRepository>().As<IReadAllRepository<RatingRecord, DifficultyLevels>>();
            builder.RegisterType<SavepointRepository>().As<IRepository<Savepoint>>();
            builder.RegisterType<JsonFileHandler<RatingRecord>>().As<IJsonFileHandler<RatingRecord>>();
            builder.RegisterType<JsonFileHandler<Savepoint>>().As<IJsonFileHandler<Savepoint>>();

            // Game Tools:
            builder.RegisterType<SolutionVerifier>().As<ISolutionVerifier>()
                .WithParameter((pi, ctx) => pi.ParameterType == typeof(DashboardItemCollection),
                                  (pi, ctx) => ctx.Resolve<DashboardItemCollection>());


            return builder.Build();
        }
    }
}
