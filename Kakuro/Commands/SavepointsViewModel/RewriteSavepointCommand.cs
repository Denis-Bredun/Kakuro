﻿using Kakuro.Base_Classes;
using Kakuro.Interfaces.Data_Access.Data_Providers;
using Kakuro.Interfaces.Game_Tools;
using System.ComponentModel;

namespace Kakuro.Commands.SavepointsViewModel
{
    public class RewriteSavepointCommand : RelayCommand, IDisposable
    {
        private ViewModels.SavepointsViewModel _savepointsViewModel;
        private readonly ViewModels.DashboardViewModel _dashboardViewModel;
        private readonly ISavepointProvider _savepointProvider;
        private readonly IOperationNotifier _operationNotifier;
        private bool _disposed = false;

        public RewriteSavepointCommand(
            ViewModels.SavepointsViewModel savepointsViewModel,
            ViewModels.DashboardViewModel dashboardViewModel,
            ISavepointProvider savepointProvider,
            IOperationNotifier operationNotifier)
        {
            _savepointsViewModel = savepointsViewModel;
            _dashboardViewModel = dashboardViewModel;
            _savepointProvider = savepointProvider;
            _operationNotifier = operationNotifier;

            _savepointsViewModel.PropertyChanged += OnSelectedSavepointPropertyChanged;
        }

        public override void Execute(object? parameter)
        {
            var savepointToRewrite = _savepointProvider.GetById(_savepointsViewModel.SelectedSavepoint.Id);
            savepointToRewrite.Dashboard = _dashboardViewModel.CreateDashboardCopy();
            _savepointProvider.Update(savepointToRewrite);
            _operationNotifier.NotifySuccess("Dashboard was successfully rewritten!");
        }

        public override bool CanExecute(object? parameter)
        {
            return AnySavepointSelected() && !AreCorrectAnswersShown() && base.CanExecute(parameter);
        }

        private bool AnySavepointSelected() => _savepointsViewModel.SelectedSavepoint != null;

        private bool AreCorrectAnswersShown() => _savepointsViewModel.CorrectAnswersAreShown;

        private void OnSelectedSavepointPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_savepointsViewModel.SelectedSavepoint) ||
                e.PropertyName == nameof(_savepointsViewModel.CorrectAnswersAreShown))
                OnCanExecutedChanged();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _savepointsViewModel.PropertyChanged -= OnSelectedSavepointPropertyChanged;
                }
                _disposed = true;
            }
        }

        ~RewriteSavepointCommand()
        {
            Dispose(false);
        }
    }
}
