using System;
using bramkominatorMobile.Services;
using bramkominatorMobile.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Linq;
using MvvmHelpers.Commands;
using Xamarin.Forms;
using bramkominatorMobile.Views;

namespace bramkominatorMobile.ViewModels
{
    public class SelectCustomCircutPageViewModel : ViewModelBase
    {
        private LogicCircut selectedCircut;
        public LogicCircut SelectedCircut { get => selectedCircut; set => SetProperty(ref selectedCircut, value); }

        public IEnumerable<LogicCircut> Circuts { get; set; }
        public int Count { get; set; }

        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand<object> SelectedItemCommand { get; set; }

        public SelectCustomCircutPageViewModel()
        {
            Title = "Saved Circuts:";

            _ = Init();

            //Circuts = new List<LogicCircut>()
            //{
            //    new LogicCircut(),
            //    new LogicCircut(),
            //    new LogicCircut(),
            //    new LogicCircut(),
            //    new LogicCircut(),
            //};

            //for (int i=0; i<5; i++)
            //{
            //    Circuts.ElementAt(i).Name = $"Circut-{i}";
            //}

            RefreshCommand = new AsyncCommand(Init);
            SelectedItemCommand = new AsyncCommand<object>(SelectedItem);
        }

        private async Task Init()
        {
            Circuts = await CircutsDbService.GetAllCircuts();
            Count = Circuts.Count();
            //Circuts = await CircutsDbService.GetAllCircutsSample();
        }

        private async Task SelectedItem(object args)
        {
            var circut = args as LogicCircut;

            if (circut == null)
                return;

            selectedCircut = null;

            await Shell.Current.DisplayAlert("Clicked", $"You have clicked {circut.Name}", "OK");
            //await Shell.Current.GoToAsync($"//{nameof(MainPage)}?CircutId={circut.Id}");
        }
    }
}
