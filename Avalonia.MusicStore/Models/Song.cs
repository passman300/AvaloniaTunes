using iTunesSearch.Library;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Avalonia.MusicStore.Models;

public class Song
{
    private static iTunesSearchManager s_SearchManager = new(); // if static, the naming will have s_
    
    // variables used to retrieve the image from the web
    private static HttpClient s_httpClient = new();
    private string CachePath => $"./Cache/{Artist} - {Title}";
    
    public string Artist { get; set; }
    public string Title { get; set; }
    public string CoverUrl { get; set; }
    
    public long ID { get; set; }
    
    
    public Song(string artist, string title, string coverUrl, long id = -1)
    {
        Artist = artist;
        Title = title;
        CoverUrl = coverUrl;
        ID = id;
    }
    
    public static async Task<IEnumerable<Song>> SearchAsync(string searchTerm)
    {   
        // first query and search for all songs in the libary using the PAO key, and save to a list
        var query = await s_SearchManager.GetSongsAsync(searchTerm)
            .ConfigureAwait(false);
                
        return query.Songs.Select(x =>
            new Song(x.ArtistName, x.CollectionName, 
                x.ArtworkUrl100.Replace("100x100bb", "600x600bb"), x.TrackId));
    }

    /// <summary>
    /// Download the image from the web and save to a file
    /// </summary>
    public async Task<Stream> LoadOverBitmapAsync()
    {
        // check if the file exist
        if (File.Exists(CachePath + ".bmp"))
        {
            return File.OpenRead(CachePath + ".bmp");
        }
        
        // otherwise download the image as array of bytes
        var data = await s_httpClient.GetByteArrayAsync(CoverUrl);
        return new MemoryStream(data);
    }
    
    

}