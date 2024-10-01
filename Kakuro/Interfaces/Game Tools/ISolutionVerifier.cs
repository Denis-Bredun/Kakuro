namespace Kakuro.Interfaces.Game_Tools
{
    public interface ISolutionVerifier
    {
        bool VerifyDashboardValues(out string message);
    }
}
