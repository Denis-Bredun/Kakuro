namespace Kakuro.Interfaces.Game_Tools
{
    public interface ISolutionVerifier
    {
        bool ValidateDashboard(out string message);
    }
}
