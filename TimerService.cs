using System;
using System.Threading;
using System.Threading.Tasks;
using DistributedTimer.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DistributedTimer
{
    public class TimerService : BackgroundService
    {
        private readonly Timer timer;

        // private Task task;
        // private bool running = false;
        // private IHubContext<TimerHub> hubContext;
        // private readonly ILogger<TimerService> logger;
        // private DateTime endTime;

        // public TimerService(IHubContext<TimerHub> hubContext, ILogger<TimerService> logger)
        // {
        //     this.hubContext = hubContext;
        //     this.logger = logger;
        // }

        // public Task StartAsync(CancellationToken cancellationToken)
        // {
        //     logger.LogInformation("Timer service started");
        //     task = new Task(Run);
        //     running = true;
        //     task.Start();
        //     return Task.CompletedTask;
        // }

        // internal void SetDuration(TimeSpan duration)
        // {
        //     endTime = DateTime.Now + duration;
        // }

        // public Task StopAsync(CancellationToken cancellationToken)
        // {
        //     logger.LogInformation("Timer service stopped");
        //     running = false;
        //     return Task.CompletedTask;
        // }

        // private void Run()
        // {
        //     while (running)
        //     {
        //         try
        //         {
        //             var remainingTime = endTime - DateTime.Now;
        //             hubContext.Clients.All.SendAsync("UpdateTime", remainingTime.ToString(@"mm\:ss"));
        //             Thread.Sleep(100);
        //         }
        //         catch (Exception ex)
        //         {
        //             logger.LogError("Exception thrown in timer service task\r\n" + ex);
        //         }
        //     }
        //     logger.LogInformation("Exiting timer service task");
        // }

        public TimerService(Timer timer)
        {
            this.timer = timer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await timer.Execute();
            }
        }
    }
}