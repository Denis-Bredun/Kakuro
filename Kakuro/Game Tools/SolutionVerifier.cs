using Kakuro.Enums;
using Kakuro.Interfaces.Game_Tools;
using Kakuro.ViewModels;

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
        public bool VerifyDashboardValues(out string message)
        {
            if (!IsDashboardFilled(out message))
                return false;

            if (!AreValuesUnique(out message))
                return false;

            if (!ValidateSums(out message))
                return false;

            message = "Game is completed!"; // #BAD: i need to make the font of message in MessageBox - bigger
            return true;
        }

        private bool IsDashboardFilled(out string message)
        {
            for (int i = 0; i < _dashboard.Count; i++)
                for (int j = 0; j < _dashboard.Count; j++)
                    if (IsValueCellEmpty(_dashboard[i][j]))
                    {
                        message = "Dashboard isn't filled completely!";
                        return false;
                    }

            message = "Dashboard is filled completely!";

            return true;
        }

        // #BAD: we need to make sure that we bind not to the field values of class,
        // but to the values ​​in the text fields themselves

        private bool IsValueCellEmpty(DashboardItemViewModel cell) => cell.CellType == CellType.ValueCell && cell.HiddenValue == "";

        public bool AreValuesUnique(out string message)
        {
            for (int i = 1; i < _dashboard.Count - 1; i++)
            {
                for (int j = 1; j < _dashboard.Count - 1; j++)
                {
                    var currentElement = _dashboard[i][j];

                    if (currentElement.CellType == CellType.ValueCell)
                    {
                        string currentValue = currentElement.HiddenValue;
                        if (!IsValueUnique(_dashboard, i, j, currentValue))
                        {
                            message = "Entered values aren't unique!";
                            return false;
                        }
                    }
                }
            }

            message = "Entered values are unique!";
            return true;
        }

        private bool IsValueUnique(DashboardItemCollection dashboard, int i, int j, string value)
        {
            return IsUniqueAbove(dashboard, i, j, value) && IsUniqueBelow(dashboard, i, j, value)
                && IsUniqueLeft(dashboard, i, j, value) && IsUniqueRight(dashboard, i, j, value);
        }

        private bool IsUniqueAbove(DashboardItemCollection dashboard, int i, int j, string value)
        {
            string valueAbove = dashboard[i - 1][j].HiddenValue;
            return valueAbove != value;
        }

        private bool IsUniqueBelow(DashboardItemCollection dashboard, int i, int j, string value)
        {
            string valueBelow = dashboard[i + 1][j].HiddenValue;
            return valueBelow != value;
        }

        private bool IsUniqueLeft(DashboardItemCollection dashboard, int i, int j, string value)
        {
            string valueLeft = dashboard[i][j - 1].HiddenValue;
            return valueLeft != value;
        }

        private bool IsUniqueRight(DashboardItemCollection dashboard, int i, int j, string value)
        {
            string valueRight = dashboard[i][j + 1].HiddenValue;
            return valueRight != value;
        }

        private bool ValidateSums(out string message)
        {
            bool areVerticalSumsCorrect = ValidateSums(true);
            bool areHorizontalSumsCorrect = ValidateSums(false);

            if (!areHorizontalSumsCorrect || !areVerticalSumsCorrect)
            {
                message = "The sum of the numbers is not equal to the given sum!";
                return false;
            }


            message = "The sum of the numbers is equal to the given sum!";
            return true;
        }

        private bool ValidateSums(bool isVerticalSum)
        {
            for (int i = 0; i < _dashboard.Count; i++)
            {
                int calculatedSum = 0;
                bool wasSumCollected = false;

                for (int j = _dashboard.Count - 1; j >= 0; j--)
                {
                    var currentElement = isVerticalSum ? _dashboard[j][i] : _dashboard[i][j];

                    if (currentElement.CellType == CellType.ValueCell)
                    {
                        calculatedSum += Convert.ToInt32(currentElement.HiddenValue);
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        var sumToCheck = isVerticalSum ? Convert.ToInt32(currentElement.SumBottom) : Convert.ToInt32(currentElement.SumRight);

                        if (sumToCheck != calculatedSum)
                            return false;

                        calculatedSum = 0;
                        wasSumCollected = false;
                    }
                }
            }
            return true;
        }
    }
}
