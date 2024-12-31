using Avalonia.Media.Imaging;
using Avalonia.MusicStore.Models;
using ReactiveUI;
using System.Threading.Tasks;
namespace Avalonia.MusicStore.ViewModels;

public class SongViewModel : ViewModelBase
{
    private readonly Song _song;
    
    public SongViewModel(Song song)
    {
        _song = song;
    }
    
    public string Artist => _song.Artist;
    public string Title => _song.Title;
    
    public string CoverUrl => _song.CoverUrl;
    
    private Bitmap? _coverImage = null;

    public Bitmap? CoverImage
    {
        get => _coverImage;
        set => this.RaiseAndSetIfChanged(ref _coverImage, value);
    }
    
    public async Task LoadCover()
    {
        await using (var imageStream = await _song.LoadOverBitmapAsync()) 
        {
            CoverImage = await Task.Run(() => Bitmap.DecodeToWidth(imageStream, 400));
        }
    }
}