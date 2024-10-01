using Kakuro.Interfaces.Game_Tools;

namespace Kakuro.Game_Tools
{
    // #BAD: tests shall be written
    public class SolutionVerifier : ISolutionVerifier
    {
        private DashboardItemCollection _dashboard;

        public SolutionVerifier(DashboardItemCollection dashboard)
        {
            _dashboard = dashboard;
        }
        public bool VerifyDashboardValues()
        {
            return false;
        }
    }
}
