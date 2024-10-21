using Kakuro.Base_Classes;
using Kakuro.Custom_Components;

namespace Kakuro.Commands.DashboardViewModel
{
    public class OpenRulesCommand : RelayCommand
    {
        public override void Execute(object? parameter)
        {
            var rulesWindow = new RulesWindow();
            rulesWindow.ShowDialog();
        }
    }
}
