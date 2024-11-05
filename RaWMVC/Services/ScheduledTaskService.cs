using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RaWMVC.Data;

public class StoryDeletionService : IHostedService, IDisposable
{
    private Timer _timer;
    private readonly IServiceScopeFactory _scopeFactory;

    public StoryDeletionService(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        //=== Configure Timer to execute DeleteScheduledStories every hour ===//
        _timer = new Timer(DeleteScheduledStories, null, TimeSpan.Zero, TimeSpan.FromHours(1));
        return Task.CompletedTask;
    }

    private async void DeleteScheduledStories(object state)
    {
        using (var scope = _scopeFactory.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<RaWDbContext>();

            var now = DateTime.Now;
            var scheduledDeletes = await context.ScheduledDeletes
                .Where(sd => sd.ScheduledTime <= now)
                .ToListAsync();

            foreach (var scheduledDelete in scheduledDeletes)
            {
                var story = await context.Stories.FindAsync(scheduledDelete.StoryId);
                if (story != null)
                {
                    context.Stories.Remove(story);
                    context.ScheduledDeletes.Remove(scheduledDelete);
                }
            }

            await context.SaveChangesAsync();
        }
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        _timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _timer?.Dispose();
    }
}
