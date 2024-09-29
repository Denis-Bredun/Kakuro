using Kakuro.Commands;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Moq;
using System.Windows.Input;
using DashboardItemCollection = System.Collections.ObjectModel.ObservableCollection<System.Collections.ObjectModel.ObservableCollection<Kakuro.ViewModels.DashboardItemViewModel>>;

namespace Kakuro.Tests.Integration_Tests
{
    public class ApplyDifficultyCommandTests : IDisposable
    {
        private IDashboardTemplateProvider _dashboardTemplateProvider;
        private DashboardItemCollection _dashboardItemCollection;
        private IDashboardProvider _dashboardProvider;
        private ICommand _applyDifficultyCommand;

        public ApplyDifficultyCommandTests()
        {
            _dashboardItemCollection = new DashboardItemCollection();
            _dashboardTemplateProvider = new DashboardTemplateProvider();
            _dashboardProvider = new DashboardProvider(_dashboardTemplateProvider, _dashboardItemCollection);
            _applyDifficultyCommand = new ApplyDifficultyCommand(_dashboardProvider);
        }

        public void Dispose()
        {
            _dashboardItemCollection.Clear();
        }

        [Fact]
        public void Should_CallGenerateDashboard_When_Executed()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Normal;
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object);

            // Act
            applyDifficultyCommand.Execute(difficultyLevel);

            // Assert
            dashboardProviderMock.Verify(dp => dp.GenerateDashboard(difficultyLevel), Times.Once);
        }

        [Fact]
        public void Should_Initialize_When_DashboardProviderProvided()
        {
            // Arrange
            var dashboardProviderMock = new Mock<IDashboardProvider>();

            // Act
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object);

            // Assert
            Assert.NotNull(applyDifficultyCommand);
        }

        [Fact]
        public void Should_ThrowNullReferenceException_When_ParameterIsNull()
        {
            // Arrange
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object);

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => applyDifficultyCommand.Execute(null));
            Assert.Equal("Parameter for ApplyDifficultyCommand is null!", exception.Message);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_ParameterIsInvalid()
        {
            // Arrange
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object);
            var invalidParameter = new object();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => applyDifficultyCommand.Execute(invalidParameter));
            Assert.Equal("Parameter for ApplyDifficultyCommand is of incorrect type!", exception.Message);
        }

        [Fact]
        public void Should_Not_CallGenerateDashboard_Twice_For_SameDifficultyLevel()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Normal;
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            dashboardProviderMock.SetupSequence(dp => dp.GetDashboardCount()).Returns(0).Returns(12); // Normal difficulty size - 10 + sum of sizes of borders from 2 sides - 2 = 12
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object);

            // Act
            applyDifficultyCommand.Execute(difficultyLevel); // First call
            applyDifficultyCommand.Execute(difficultyLevel); // Second call (should not generate again)

            // Assert
            dashboardProviderMock.Verify(dp => dp.GenerateDashboard(difficultyLevel), Times.Once);
        }
    }
}
