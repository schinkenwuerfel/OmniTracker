using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using OmniTracker.DataContext;
using OmniTracker.DataContext.Models;
using OmniTracker.Extensions;
using System.Collections.ObjectModel;

namespace OmniTracker;
public partial class MainPageViewModel(IServiceProvider serviceProvider) : ObservableObject
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(AddTrackerCommand))]
    private string _NewTrackerEntry = string.Empty;

    public ObservableCollection<TrackerViewModel> Trackers { get; } = new(serviceProvider.GetRequiredService<TrackerContext>().Trackers.Include(x => x.Entries).Select(x => new TrackerViewModel(x, serviceProvider)));

    [RelayCommand(CanExecute = nameof(canAddTracker))]
    public async Task AddTracker()
    {
        var tracker = new Tracker { Name = NewTrackerEntry };
        try
        {
            var dataContext = serviceProvider.GetRequiredService<TrackerContext>();
            dataContext.Trackers.Add(tracker);
            await dataContext.SaveChangesAsync();
            var viewModel = new TrackerViewModel(tracker, serviceProvider);
            Trackers.Add(viewModel);
            NewTrackerEntry = string.Empty;
        }
        catch (Exception)
        {
            Toast.Make("Error while saving Tracker to DB");
        }
    }

    [RelayCommand]
    public async Task RemoveTracker(TrackerViewModel tracker)
    {
        try
        {
            var dataContext = serviceProvider.GetRequiredService<TrackerContext>();
            dataContext.Trackers.Remove(tracker.Tracker);
            await dataContext.SaveChangesAsync();
            Trackers.Remove(tracker);
        }
        catch (Exception)
        {
            Toast.Make("Error while saving Tracker to DB");
        }
    }

    private bool canAddTracker() => NewTrackerEntry.IsNotNullOrEmpty();
}
