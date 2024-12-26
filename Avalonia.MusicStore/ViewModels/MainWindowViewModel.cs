using System;
using System.Collections.Generic;
using System.Reactive.Linq;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace Avalonia.MusicStore.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        /*public MainWindowViewModel()
        {
            ShowDialog = new Interaction<MusicStoreViewModel, SongViewModel?>();

            BuyMusicCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                var store = new MusicStoreViewModel();

                var result = await ShowDialog.Handle(store);
            });
        }*/
        
        [ObservableProperty]
        private bool _isPaneOpen = true;
        
        // TODO
        [RelayCommand]
        public void TriggerPaneComand()
        {
            IsPaneOpen = !IsPaneOpen;
            //https://www.youtube.com/watch?v=UDbKVheMBY8&list=PLJYo8bcmfTDF6ROxC8QMVw9Zr_3Lx4Lgd&index=3
        }
        
        /*public ICommand BuyMusicCommand { get; }

        public Interaction<MusicStoreViewModel, SongViewModel?> ShowDialog { get; }*/
    
        
    }
}