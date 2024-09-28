using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Tests.Integration_Tests
{
    public class DashboardProviderTests
    {
        private IDashboardProvider _dashboardProvider;

        public DashboardProviderTests(IDashboardProvider dashboardProvider)
        {
            _dashboardProvider = dashboardProvider;
        }


    }
}
