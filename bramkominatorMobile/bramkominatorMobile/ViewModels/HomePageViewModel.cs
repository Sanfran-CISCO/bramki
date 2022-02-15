using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using bramkominatorMobile.Models;
using bramkominatorMobile.Services;
using bramkominatorMobile.Views;
using MvvmHelpers;
using MvvmHelpers.Commands;
using Xamarin.Forms;

namespace bramkominatorMobile.ViewModels
{
    public class HomePageViewModel : ViewModelBase
    {
        public int CircutId { get; set; }

        public IEnumerable<LogicCircut> Circuts { get; set; }

        public AsyncCommand CreateNewWorkspaceCommand { get; }
        public AsyncCommand LoadWorkspaceCommand { get; }
        public HomePageViewModel()
        {
            Title = "Bramkominator";

            CreateNewWorkspaceCommand = new AsyncCommand(CreateWorkspace);
            LoadWorkspaceCommand = new AsyncCommand(LoadWorkspace);
        }

        private async Task CreateWorkspace()
        {
            await Shell.Current.GoToAsync($"//{nameof(MainPage)}?CircutId=-1");
        }

        private async Task LoadWorkspace()
        {
            await Shell.Current.GoToAsync($"{nameof(SelectCustomCircutPage)}");
        }
    }
}
