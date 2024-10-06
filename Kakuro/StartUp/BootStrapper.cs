using Autofac;
using Kakuro.Commands;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Data_Access.Tools;
using Kakuro.Game_Tools;
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

            builder.RegisterType<MainWindow>().AsSelf().SingleInstance();

            builder.Register(c => new DashboardItemCollection())
                   .AsSelf()
                   .SingleInstance();


            // ViewModels
            builder.RegisterType<MainViewModel>().AsSelf().SingleInstance();
            builder.RegisterType<DashboardItemViewModel>().As<IDashboardItemViewModel>();
            builder.RegisterType<DashboardViewModel>()
           .As<IDashboardViewModel>()
           .SingleInstance();

            // Commands
            builder.RegisterType<CleanDashboardCommand>().AsSelf().SingleInstance();
            builder.RegisterType<VerifySolutionCommand>().AsSelf().SingleInstance();

            // Data Access
            builder.RegisterAssemblyTypes(typeof(DashboardProvider).Assembly)
           .Where(t => t.Name.EndsWith("Provider") || t.Name.EndsWith("Repository"))
           .AsImplementedInterfaces()
           .SingleInstance();
            builder.RegisterType<JsonFileHandler<RatingRecord>>().As<IJsonFileHandler<RatingRecord>>().SingleInstance();
            builder.RegisterType<JsonFileHandler<Savepoint>>().As<IJsonFileHandler<Savepoint>>().SingleInstance();

            // Game Tools:
            builder.RegisterType<SolutionVerifier>().As<ISolutionVerifier>().SingleInstance();
            builder.RegisterType<OperationNotifier>().As<IOperationNotifier>().SingleInstance();


            return builder.Build();
        }
    }
}
