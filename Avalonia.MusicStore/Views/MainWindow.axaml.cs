using System.Threading.Tasks;
using ReactiveUI;
using Avalonia.MusicStore.ViewModels;
using Avalonia.ReactiveUI;

namespace Avalonia.MusicStore.Views;

public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
{
    public MainWindow()
    {
        InitializeComponent();
        
        this.WhenActivated(action => 
            action(ViewModel!.ShowDialog.RegisterHandler(DoShowDialogAsync)));
    }
    
    // This code is only valid in newer ReactiveUI which is shipped since avalonia 11.2.0 
    private async Task DoShowDialogAsync(IInteractionContext<MusicStoreViewModel,
        SongViewModel?> interaction)
    {
        var dialog = new MusicStoreWindow();
        dialog.DataContext = interaction.Input;

        var result = await dialog.ShowDialog<SongViewModel?>(this);
        interaction.SetOutput(result);
    }
}