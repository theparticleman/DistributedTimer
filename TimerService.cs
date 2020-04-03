using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DistributedTimer
{
    public class TimerService : IHostedService
    {

        private readonly Timer timer;
        private readonly ILogger<TimerService> logger;
        private Task task;
        private bool running = false;

        public TimerService(Timer timer, ILogger<TimerService> logger)
        {
            this.timer = timer;
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timer service started");
            task = new Task(Run);
            running = true;
            task.Start();
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Timer service stopped");
            running = false;
            return Task.CompletedTask;
        }

        private void Run()
        {
            while (running)
            {
               timer.Execute();
               Thread.Sleep(100);
            }
            logger.LogInformation("Exiting timer service task");
        }
    }
}