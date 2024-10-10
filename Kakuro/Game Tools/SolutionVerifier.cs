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
        public bool ValidateDashboard(out string message)
        {
            if (!ValidateSums(out message))
                return false;

            message = "Game is completed!"; // #BAD: i need to make the font of message in MessageBox - bigger
            return true;
        }

        // #BAD: we need to make sure that we bind not to the field values of class,
        // but to the values ​​in the text fields themselves

        private bool ValidateSums(out string message)
        {
            bool areVerticalSumsCorrect = ValidateConcreteSum(true, out message, true);

            if (!areVerticalSumsCorrect)
                return false;

            bool areHorizontalSumsCorrect = ValidateConcreteSum(false, out message);

            if (!areHorizontalSumsCorrect)
                return false;

            return true;
        }

        private bool ValidateConcreteSum(bool isVerticalSum, out string message, bool shouldCheckValues = false)
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
                        if (shouldCheckValues)
                        {
                            if (!CheckValueForCorrectness(i, j, currentElement, isVerticalSum, out message))
                                return false;
                        }

                        calculatedSum += Convert.ToInt32(currentElement.DisplayValue);
                        wasSumCollected = true;
                    }
                    else if (wasSumCollected)
                    {
                        var sumToCheck = isVerticalSum ? Convert.ToInt32(currentElement.SumBottom) : Convert.ToInt32(currentElement.SumRight);

                        if (sumToCheck != calculatedSum)
                        {
                            message = "The sum of the numbers is not equal to the given sum!";
                            return false;
                        }

                        calculatedSum = 0;
                        wasSumCollected = false;
                    }
                }
            }

            message = "The sum of the numbers is equal to the given sum!";
            return true;
        }

        private bool CheckValueForCorrectness(int i, int j, DashboardItemViewModel currentElement, bool isVerticalSum, out string message)
        {
            if (!CheckValueForEmptiness(currentElement, out message))
                return false;

            int tempI, tempJ;

            if (isVerticalSum)
            {
                tempI = j;
                tempJ = i;
            }
            else
            {
                tempI = i;
                tempJ = j;
            }

            if (!CheckValueForUniqueness(tempI, tempJ, currentElement.DisplayValue, out message))
                return false;

            return true;
        }

        private bool CheckValueForEmptiness(DashboardItemViewModel cell, out string message)
        {
            if (cell.CellType == CellType.ValueCell && cell.DisplayValue == "")
            {
                message = "Dashboard isn't filled completely!";
                return false;
            }
            message = "Dashboard is filled completely!";
            return true;
        }

        private bool CheckValueForUniqueness(int i, int j, string value, out string message)
        {
            if (!(IsUniqueAbove(i, j, value) && IsUniqueBelow(i, j, value)
                && IsUniqueLeft(i, j, value) && IsUniqueRight(i, j, value)))
            {
                message = "Entered values aren't unique!";
                return false;
            }

            message = "Entered values are unique!";
            return true;
        }

        private bool IsUniqueAbove(int i, int j, string value)
        {
            string valueAbove = _dashboard[i - 1][j].DisplayValue;
            return valueAbove != value;
        }

        private bool IsUniqueBelow(int i, int j, string value)
        {
            string valueBelow = _dashboard[i + 1][j].DisplayValue;
            return valueBelow != value;
        }

        private bool IsUniqueLeft(int i, int j, string value)
        {
            string valueLeft = _dashboard[i][j - 1].DisplayValue;
            return valueLeft != value;
        }

        private bool IsUniqueRight(int i, int j, string value)
        {
            string valueRight = _dashboard[i][j + 1].DisplayValue;
            return valueRight != value;
        }
    }
}
