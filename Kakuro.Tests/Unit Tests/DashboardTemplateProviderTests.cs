using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;

namespace Kakuro.Tests.Unit_Tests
{
    public class DashboardTemplateProviderTests
    {
        private readonly IDashboardTemplateProvider _provider;

        public DashboardTemplateProviderTests()
        {
            _provider = new DashboardTemplateProvider();
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
