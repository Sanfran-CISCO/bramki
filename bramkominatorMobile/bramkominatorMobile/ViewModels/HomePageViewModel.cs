using System;
using System.Collections.Generic;
using System.Text;
using bramkominatorMobile.Models;
using MvvmHelpers;
using MvvmHelpers.Commands;

namespace bramkominatorMobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public Command CreateNewWorkspaceCommand { get; }
        public Command LoadWorkspaceCommand { get; }
        public HomePageViewModel()
        {
            Title = "Bramkominator";

            CreateNewWorkspaceCommand = new Command(CreateWorkspace);
            LoadWorkspaceCommand = new Command(LoadWorkspace);
        }

        private void CreateWorkspace()
        {

        }

        private void LoadWorkspace()
        {

        }
    }
}
