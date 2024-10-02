using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Data_Access.Data_Providers
{
    public class DashboardTemplateProvider : IDashboardTemplateProvider
    {
        private Random _random;

        public DashboardTemplateProvider()
        {
            _random = new Random();
        }

        public string[,] GenerateTemplate(DifficultyLevels difficultyLevel)
        {
            string[,] template = difficultyLevel switch
            {
                DifficultyLevels.Easy => GenerateEasyTemplate(),
                DifficultyLevels.Normal => GenerateNormalTemplate(),
                DifficultyLevels.Hard => GenerateHardTemplate(),
                _ => throw new ArgumentException()
            };

            InvertSymbolsByChance(template); // #BAD: the ways of template being generated should be documented

            return template;
        }

        // #BAD: i shall add test that checks if borders contain *

        private string[,] GenerateEasyTemplate() => new string[,]
        {
            { "",   "",   "",     "",     "",     "",     "",     "" },
            { "",   "",   "*",    "",     "",     "*",    "",     "" },
            { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
            { "",   "",   "*",    "",     "",     "*",    "",     "" },
            { "",   "*",  "*",    "*",    "*",    "*",    "*",    "" },
            { "",   "",   "*",    "*",    "*",    "*",    "",     "" },
            { "",   "",   "",     "*",    "*",    "",     "",     "" },
            { "",   "",   "",     "",     "",     "",     "",     "" }
        };

        private string[,] GenerateNormalTemplate() => new string[,]
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
        };

        private string[,] GenerateHardTemplate() => new string[,]
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

        private void InvertSymbolsByChance(string[,] template)
        {
            int dashboardSize = template.GetLength(0);

            for (int i = 1; i < dashboardSize - 1; i++)
                for (int j = 1; j < dashboardSize - 1; j++)
                    if (_random.Next(2) == 1)               //probability - 50%
                        template[i, j] = template[i, j] == "*" ? "" : "*";

        }
    }
}
