using System;
using System.Collections.Generic;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;
using System.Reactive.Concurrency;
using System.Threading;
using Avalonia.MusicStore.Models;

namespace Avalonia.MusicStore.ViewModels
{
    public class MusicStoreViewModel : ViewModelBase
    {
        private bool _isBusy;
        private string? _searchText;
        
        private CancellationTokenSource? _cancellationTokenSource;

        public MusicStoreViewModel()
        {
            // start the searching
            this.WhenAnyValue(x => x.SearchText)
                .Throttle(TimeSpan.FromMilliseconds(500))
                .ObserveOn(RxApp.MainThreadScheduler)
                .Subscribe(DoSearch!);
        }

        /// <summary>
        /// Search Method 
        /// </summary>
        private async void DoSearch(string? searchText)
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
            var cancellationToken = _cancellationTokenSource.Token;
            
            IsBusy = true;
            SearchResults.Clear();

            if (!string.IsNullOrWhiteSpace(searchText))
            {
                // Need to remove duplicates. 
                // This is an issue with the iTunes API, and how they handle their song libary
                var songs_temp = (await Song.SearchAsync(searchText));
                HashSet<Song> songs = new HashSet<Song>();
                foreach (var song in songs_temp)
                {
                    songs.Add(song);
                }
                
                // import each song
                foreach (var song in songs)
                {
                    var viewModel = new SongViewModel(song);
                    SearchResults.Add(viewModel);
                }
                
                if (!cancellationToken.IsCancellationRequested)
                {
                    LoadCovers(cancellationToken);
                }
            }
            
            
            IsBusy = false;
        }

    public string? SearchText
        {
            get => _searchText;
            set => this.RaiseAndSetIfChanged(ref _searchText, value);
        }

        public bool IsBusy
        {
            get => _isBusy;
            set => this.RaiseAndSetIfChanged(ref _isBusy, value);
        }
        
        private SongViewModel? _selectedSong;

        public ObservableCollection<SongViewModel> SearchResults { get; } = new();

        public SongViewModel? SelectedSong
        {
            get => _selectedSong;
            set => this.RaiseAndSetIfChanged(ref _selectedSong, value);
        }
        
        public ReactiveCommand<Unit, SongViewModel?> BuyMusicCommand { get; }
        
        private async void LoadCovers(CancellationToken cancellationToken)
        {
            foreach (var album in SearchResults.ToList())
            {
                await album.LoadCover();

                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }
            }
        }
    }
}

