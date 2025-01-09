using System.Collections.ObjectModel;
using System.Threading;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTunes.ViewModels;

public class SearchPageViewModel : ViewModelBase
{
    // Searching variables
    private bool _isSearching; // Searching status
    private string _searchQuery = string.Empty;
    
    // Selected song
    private SongViewModel? _selectedSong;
    
    // List of all songs which will be displayed
    private ObservableCollection<SongViewModel> _searchResults = new();

    private CancellationTokenSource? _cancellationTokenSource;

    public SearchPageViewModel()
    {
        SearchResults.Add(new SongViewModel());
    }
    
    public ObservableCollection<SongViewModel> SearchResults
    {
        get => _searchResults;
    }

    public SongViewModel? SelectedSong
    {
        get => _selectedSong;
        set => SetProperty(ref _selectedSong, value);
    }
    
    
    public bool IsSearching
    {
        get => _isSearching;
        set => SetProperty(ref _isSearching, value);
    }
    
    public string SearchQuery
    {
        get => _searchQuery;
        set => SetProperty(ref _searchQuery, value);
    }
}