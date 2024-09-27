using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;

namespace Kakuro.Tests.Unit_Tests
{
    public class DashboardTemplateProviderTests
    {
        private readonly DashboardTemplateProvider _provider;

        public DashboardTemplateProviderTests()
        {
            _provider = new DashboardTemplateProvider();
        }

        [Fact]
        public void GenerateTemplate_ShouldReturnEasyTemplate_WhenDifficultyIsEasy()
        {
            // Arrange
            var expectedTemplate = new string[,]
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

            // Act
            var result = _provider.GenerateTemplate(DifficultyLevels.Easy);

            // Assert
            Assert.Equal(expectedTemplate, result);
        }

        [Fact]
        public void GenerateTemplate_ShouldReturnNormalTemplate_WhenDifficultyIsNormal()
        {
            // Arrange
            var expectedTemplate = new string[,]
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

            // Act
            var result = _provider.GenerateTemplate(DifficultyLevels.Normal);

            // Assert
            Assert.Equal(expectedTemplate, result);
        }

        [Fact]
        public void GenerateTemplate_ShouldReturnHardTemplate_WhenDifficultyIsHard()
        {
            // Arrange
            var expectedTemplate = new string[,]
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

            // Act
            var result = _provider.GenerateTemplate(DifficultyLevels.Hard);

            // Assert
            Assert.Equal(expectedTemplate, result);
        }

        [Fact]
        public void GenerateTemplate_ShouldThrowNotImplementedException_WhenDifficultyIsInvalid()
        {
            // Arrange
            var invalidDifficulty = (DifficultyLevels)999;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _provider.GenerateTemplate(invalidDifficulty));
        }

    }
}
