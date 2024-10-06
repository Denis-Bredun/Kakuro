using Kakuro.Commands;
using Kakuro.Data_Access.Data_Providers;
using Kakuro.Enums;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Models;
using Kakuro.ViewModels;
using Moq;
using System.Collections.ObjectModel;
using System.Windows.Input;

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
            _applyDifficultyCommand = new ApplyDifficultyCommand(_dashboardProvider, null);
        }

        public void Dispose()
        {
            _dashboardItemCollection.Clear();
        }

        // #BAD: some tests won't work cuz' of _dashboardViewModel.ChoosenDifficulty = difficultyLevel;
        // line in ApplyDifficultyCommand

        [Fact]
        public void Should_CallGenerateDashboard_When_Executed()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Normal;
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object, null);

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
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object, null);

            // Assert
            Assert.NotNull(applyDifficultyCommand);
        }

        [Fact]
        public void Should_ThrowNullReferenceException_When_ParameterIsNull()
        {
            // Arrange
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object, null);

            // Act & Assert
            var exception = Assert.Throws<NullReferenceException>(() => applyDifficultyCommand.Execute(null));
            Assert.Equal("Parameter for ApplyDifficultyCommand is null!", exception.Message);
        }

        [Fact]
        public void Should_ThrowArgumentException_When_ParameterIsInvalid()
        {
            // Arrange
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object, null);
            var invalidParameter = new object();

            // Act & Assert
            var exception = Assert.Throws<ArgumentException>(() => applyDifficultyCommand.Execute(invalidParameter));
            Assert.Equal("Parameter for ApplyDifficultyCommand is of incorrect type!", exception.Message);
        }

        [Fact]
        public void Should_CallGenerateDashboard_Twice_For_SameDifficultyLevel()
        {
            // Arrange
            var difficultyLevel = DifficultyLevels.Normal;
            var dashboardProviderMock = new Mock<IDashboardProvider>();
            var applyDifficultyCommand = new ApplyDifficultyCommand(dashboardProviderMock.Object, null);

            dashboardProviderMock
                .Setup(dp => dp.GenerateDashboard(difficultyLevel))
                .Callback(() =>
                {
                    int fullDashboardSize = (int)difficultyLevel + 2; // 2 - two elements from 2 side borders
                    for (int i = 0; i < fullDashboardSize; i++)
                    {
                        var row = new ObservableCollection<DashboardItemViewModel>();
                        for (int j = 0; j < fullDashboardSize; j++)
                        {
                            row.Add(new DashboardItemViewModel(new DashboardItem()));
                        }
                        _dashboardItemCollection.Add(row);
                    }
                });

            // Act
            applyDifficultyCommand.Execute(difficultyLevel); // First call
            applyDifficultyCommand.Execute(difficultyLevel); // Second call 

            // Assert
            dashboardProviderMock.Verify(dp => dp.GenerateDashboard(difficultyLevel), Times.Exactly(2));
        }
    }
}
