﻿using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Data_Access.Data_Providers
{
    public class DashboardTemplateProvider : IDashboardTemplateProvider
    {
        public string[,] GenerateTemplate(DifficultyLevels difficultyLevel) => difficultyLevel switch
        {
            DifficultyLevels.Easy => GenerateEasyTemplate(),
            DifficultyLevels.Normal => GenerateNormalTemplate(),
            DifficultyLevels.Hard => GenerateHardTemplate(),
            _ => throw new NotImplementedException()
        };

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
    }
}