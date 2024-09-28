using Kakuro.Data_Access.Data_Providers;
using Kakuro.Interfaces.Data_Access.Data_Providers;

using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Tests.Integration_Tests
{
    public class DashboardProviderTests
    {
        private IDashboardTemplateProvider _dashboardTemplateProvider;
        private DashboardItemCollection _dashboardItemCollection;
        private IDashboardProvider _dashboardProvider;

        public DashboardProviderTests()
        {
            _dashboardItemCollection = new DashboardItemCollection();
            _dashboardTemplateProvider = new DashboardTemplateProvider();
            _dashboardProvider = new DashboardProvider(_dashboardTemplateProvider, _dashboardItemCollection);
        }


    }
}
