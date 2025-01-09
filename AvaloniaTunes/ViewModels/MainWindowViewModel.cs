using System;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AvaloniaTunes.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    
    [ObservableProperty]
    private bool _isPaneOpen = true; // Toggle siding bar


    [ObservableProperty] 
    private ViewModelBase _currentPage = new HomePageViewModel(); // Landing page
    
    [ObservableProperty]
    private ListItemTemplate? _selectedListItem; // Selected item in the list
    
    [RelayCommand]
    private void TogglePane()
    {
        IsPaneOpen = !IsPaneOpen;
    }

    partial void OnSelectedListItemChanged(ListItemTemplate? value)
    {
        if (value is null) return;
        var instance = (ViewModelBase) Activator.CreateInstance(value.ModelType);
        if (instance != null) CurrentPage = instance;
        else return;
    }
    
    // sidebar page items
    public ObservableCollection<ListItemTemplate> Items { get; } = new()
    {
        new ListItemTemplate(typeof(HomePageViewModel)),
        new ListItemTemplate(typeof(SearchPageViewModel))
    };
    

}

public class ListItemTemplate
{
    public ListItemTemplate(Type type)
    {
        ModelType = type;
        Name = type.Name.Replace("PageViewModel", ""); // MAKE SURE TO USE THIS NAMING CONVENTION
    }
    
    public string Name { get; set; }
    public Type ModelType { get; set; }

}