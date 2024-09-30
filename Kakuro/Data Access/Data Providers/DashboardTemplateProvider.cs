﻿using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Data_Access.Data_Providers
{
    public class DashboardTemplateProvider : IDashboardTemplateProvider
    {
        public string[,] GenerateTemplate(DifficultyLevels difficultyLevel)
        {
            string[,] template = difficultyLevel switch
            {
                DifficultyLevels.Easy => GenerateEasyTemplate(),
                DifficultyLevels.Normal => GenerateNormalTemplate(),
                DifficultyLevels.Hard => GenerateHardTemplate(),
                _ => throw new ArgumentException()
            };

            return RotateTemplateRandomly(template); // #BAD: the ways of template being generated should be documented
        }

        private string[,] GenerateEasyTemplate()
        {
            string[][,] templates =
            [
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "*",    "",     "",     "*",    "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "",     "",     "*",    "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "",     "" },
                    { "",   "",   "",     "*",    "*",    "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                },
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "*",  "*",    "",     "*",    "",     "*",    "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "",     "" },
                    { "",   "*",  "",     "",     "*",    "",     "*",    "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "",     "" },
                    { "",   "*",  "",     "",     "",     "*",    "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                },
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "*",    "*",    "*",    "",     "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "",     "*",    "",     "",     "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "",     "*",    "*",    "*",    "",     "" },
                    { "",   "",   "",     "",     "",     "*",    "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                },
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "*",  "*",    "*",    "",     "*",    "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "",     "*",    "",     "",     "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "",     "",     "*",    "*",    "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                },
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "*",  "*",    "",     "",     "*",    "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "*",    "*",    "",     "*",    "",     "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "*",  "*",    "",     "",     "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                },
                new string[,]
                {
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "*",    "*",    "",     "",     "",     "" },
                    { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "*",  "",     "*",    "",     "*",    "",     "" },
                    { "",   "",   "*",    "*",    "*",    "*",    "*",    "" },
                    { "",   "",   "",     "*",    "*",    "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" },
                    { "",   "",   "",     "",     "",     "",     "",     "" }
                }
            ];

            Random random = new Random();
            int randomIndex = random.Next(templates.Length);
            return templates[randomIndex];
        }



        private string[,] GenerateNormalTemplate()
        {
            string[][,] templates =
            [
                new string[,]
                {
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" },
                    { "",    "*",   "",    "",      "*",     "",      "",      "*",     "",      "",      "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "",      "",      "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "*",   "*",     "*",     "",      "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "",    "*",   "*",     "*",     "",      "",      "*",     "*",     "*",     "",       "" },
                    { "",    "*",   "",    "*",     "*",     "*",     "*",     "*",     "*",     "",      "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "*",     "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "*",     "",      "",      "*",     "*",     "",      "*",      "" },
                    { "",    "",    "*",   "*",     "",      "",      "",      "",      "*",     "*",     "",       "" },
                    { "",    "*",   "*",   "",      "*",     "",      "",      "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "",    "",      "",      "*",     "*",     "",      "",      "",      "*",      "" },
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" }
                },
                new string[,]
                {
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" },
                    { "",    "",    "*",   "",      "*",     "",      "*",     "",      "*",     "",      "",       "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "*",     "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "",      "*",     "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "*",   "*",     "*",     "",      "",      "*",     "*",     "",      "",       "" },
                    { "",    "",    "*",   "",      "*",     "*",     "*",     "",      "",      "*",     "*",      "" },
                    { "",    "*",   "",    "",      "*",     "",      "*",     "*",     "*",     "",      "",       "" },
                    { "",    "*",   "",    "*",     "*",     "",      "",      "*",     "*",     "",      "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "",      "",      "*",     "",      "*",      "" },
                    { "",    "",    "*",   "*",     "",      "",      "",      "*",     "",      "*",     "*",      "" },
                    { "",    "",    "",    "",      "",      "*",     "",      "",      "",      "",      "*",      "" },
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" }
                },
                new string[,]
                {
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" },
                    { "",    "*",   "*",   "",      "*",     "",      "",      "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "*",     "*",     "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "",    "*",   "*",     "*",     "",      "*",     "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "*",     "",      "",      "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "",      "*",     "*",     "*",     "*",     "",      "*",      "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "",      "*",     "",      "*",     "*",      "" },
                    { "",    "",    "*",   "",      "*",     "",      "*",     "*",     "*",     "",      "*",      "" },
                    { "",    "",    "",    "*",     "",      "*",     "",      "*",     "*",     "",      "*",      "" },
                    { "",    "",    "*",   "*",     "*",     "*",     "",      "",      "*",     "",      "*",      "" },
                    { "",    "*",   "",    "",      "*",     "*",     "",      "*",     "",      "*",     "",       "" },
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" }
                },
                new string[,]
                {
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" },
                    { "",    "*",   "",    "*",     "*",     "*",     "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "*",     "",      "*",     "*",     "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "",      "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "*",     "*",     "*",     "",      "",      "",      "*",      "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "*",     "*",     "*",     "*",     "*",      "" },
                    { "",    "",    "",    "*",     "*",     "*",     "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "",    "",      "*",     "*",     "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "*",     "",      "*",     "*",     "*",      "" },
                    { "",    "",    "*",   "*",     "*",     "",      "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" }
                },
                new string[,]
                {
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" },
                    { "",    "*",   "*",   "*",     "*",     "",      "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "",    "",      "*",     "",      "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "*",   "*",   "*",     "*",     "*",     "*",     "*",     "*",     "*",     "*",      "" },
                    { "",    "",    "*",   "*",     "*",     "",      "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "*",     "",      "*",     "*",     "*",      "" },
                    { "",    "*",   "",    "*",     "",      "",      "*",     "*",     "",      "*",     "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "*",     "*",     "*",     "*",     "*",     "*",      "" },
                    { "",    "",    "*",   "*",     "*",     "*",     "",      "*",     "*",     "*",     "*",      "" },
                    { "",    "*",   "*",   "",      "*",     "",      "*",     "*",     "*",     "*",     "",       "" },
                    { "",    "*",   "",    "*",     "*",     "",      "*",     "*",     "*",     "*",     "*",      "" },
                    { "",    "",    "",    "",      "",      "",      "",      "",      "",      "",      "",       "" }
                }
            ];

            Random random = new Random();
            int randomIndex = random.Next(templates.Length);
            return templates[randomIndex];
        }


        private string[,] GenerateHardTemplate()
        {
            Random random = new Random();
            var hardTemplate = new string[,]
            {
                { "",    "",    "",    "",    "",       "",       "",      "",      "",      "",      "",      "",       "",       "",      "",       "",       "",    ""  },
                { "",    "",    "",    "*",   "*",      "",       "",      "",      "",      "",      "*",     "*",      "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "",      "",      "",      "*",     "*",     "*",      "",       "",      "",       "*",      "*",   ""  },
                { "",    "",    "",    "*",   "*",      "*",      "*",     "",      "*",     "*",     "*",     "",       "*",      "*",     "*",      "",       "*",   ""  },
                { "",    "",    "",    "",    "*",      "*",      "",      "",      "",      "*",     "",      "*",      "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "",    "",    "*",   "*",      "*",      "*",     "",      "*",     "*",     "",      "*",      "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "",    "*",   "*",   "*",      "",       "*",     "*",     "*",     "*",     "",      "*",      "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "",    "",    "*",   "*",      "",       "",      "",      "*",     "*",     "*",     "",       "*",      "*",     "*",      "",       "*",   ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "*",     "",      "",      "",      "*",     "*",      "",       "",      "",       "*",      "*",   ""  },
                { "",    "*",   "*",   "",    "",       "",       "*",     "*",     "*",     "",      "*",     "*",      "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "*",   "",    "*",   "*",      "*",      "",      "*",     "*",     "",      "",      "",       "",       "*",     "*",      "",       "",    ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "*",     "",      "*",     "*",     "*",     "*",      "",       "*",     "*",      "*",      "",    ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "*",     "",      "*",     "*",     "",      "*",      "*",      "*",     "*",      "",       "",    ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "*",     "",      "*",     "",      "",      "",       "*",      "*",     "",       "",       "",    ""  },
                { "",    "*",   "",    "*",   "*",      "*",      "",      "*",     "*",     "*",     "",      "*",      "*",      "*",     "*",      "",       "",    ""  },
                { "",    "*",   "*",   "",    "",       "",       "*",     "*",     "*",     "",      "",      "",       "*",      "*",     "*",      "*",      "",    ""  },
                { "",    "",    "*",   "*",   "*",      "*",      "*",     "*",     "",      "",      "",      "",       "",       "*",     "*",      "",       "",    ""  },
                { "",    "",    "",    "",    "",       "",       "",      "",      "",      "",      "",      "",       "",       "",      "",       "",       "",    ""  }
            };

            if (random.NextDouble() < 0.5)
                hardTemplate = InvertTemplateSymbols(hardTemplate);

            return hardTemplate;
        }

        private string[,] RotateTemplate90DegreesClockwise(string[,] template)
        {
            int rows = template.GetLength(0);
            int cols = template.GetLength(1);
            string[,] rotatedTemplate = new string[cols, rows];

            for (int i = 0; i < rows; i++)
                for (int j = 0; j < cols; j++)
                    rotatedTemplate[j, rows - 1 - i] = template[i, j];

            return rotatedTemplate;
        }

        private string[,] RotateTemplateRandomly(string[,] template)
        {
            Random random = new Random();
            int rotations = random.Next(1, 5);

            string[,] rotatedTemplate = template;

            for (int i = 0; i < rotations; i++)
                rotatedTemplate = RotateTemplate90DegreesClockwise(rotatedTemplate);

            return rotatedTemplate;
        }

        private string[,] InvertTemplateSymbols(string[,] template)
        {
            int rows = template.GetLength(0);
            int cols = template.GetLength(1);
            var invertedTemplate = new string[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (IsBorderCell(i, j, rows, cols))
                        invertedTemplate[i, j] = template[i, j];
                    else
                        invertedTemplate[i, j] = template[i, j] == "*" ? "" : "*";
                }
            }

            return invertedTemplate;
        }

        private bool IsBorderCell(int row, int col, int totalRows, int totalCols)
        {
            return row == 0 || row == totalRows - 1 || col == 0 || col == totalCols - 1;
        }
    }
}
