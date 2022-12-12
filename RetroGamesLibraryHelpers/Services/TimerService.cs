using static System.GC;

namespace RetroGamesLibraryHelpers.Services;

public class TimerService
{
    public PeriodicTimer Timer;
    // NOTE: I would rather have the task be sent in to the constructor
    // But I do not yet know how to to that without having the task executing its code.
    public Task? TimerTask;
    public CancellationTokenSource CancellationTokenSource;

    public TimerService(PeriodicTimer timer)
    {
        CancellationTokenSource = new CancellationTokenSource();
        Timer = timer;
    }

    public void Cancel() => CancellationTokenSource.Cancel();

    public async Task HandleAsyncTask(Task task)
    {
        try
        {
            TimerTask = task;
            // NOTE: The task is being executed as it is being defined, do not know how to get that to work.
            // If it would work it should also have the timer initialization here in stead of in the task.
            await task;
        }
        catch (TaskCanceledException)
        {

            await DisposeAsync();

        }
        catch (Exception ex)
        {
            // TODO: Loggning not implemented yet (both exceptions should be logged)
        }
    }

    public async ValueTask DisposeAsync()
    {
        Timer.Dispose();
        if (TimerTask != null)
        {
            await TimerTask;
        }
        SuppressFinalize(this);
    }
}