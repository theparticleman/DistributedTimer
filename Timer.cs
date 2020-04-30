using System;
using System.Diagnostics;
using DistributedTimer.Hubs;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace DistributedTimer
{
    public class Timer
    {
        private readonly IHubContext<TimerHub> timerHub;
        private readonly ILogger<Timer> logger;
        private Stopwatch timer;
        private TimeSpan duration;

        private bool timerFinished;

        public Timer(IHubContext<TimerHub> timerHub, ILogger<Timer> logger)
        {
            this.timerHub = timerHub;
            this.logger = logger;
            timer = new Stopwatch();
            duration = TimeSpan.FromMinutes(10);
        }

        internal void Execute()
        {
            var updateEvent = new TimerEvent();
            if (duration == default)
            {
                updateEvent.Message = "waiting for timer to start";
            }
            else
            {
                var remainingTime = duration - timer.Elapsed;
                string result = remainingTime.TotalMilliseconds > 0 ? remainingTime.ToString(@"mm\:ss") : "00:00";
                updateEvent.Message = result;
                updateEvent.PauseEnabled = timer.IsRunning;
                updateEvent.ResumeEnabled = !timer.IsRunning;
                updateEvent.RestartEnabled = true;
                if (remainingTime.TotalMilliseconds <= 0 && !timerFinished)
                {
                    timerFinished = true;
                    timerHub.Clients.All.SendAsync("TimerElapsed", new { });
                }
            }
            timerHub.Clients.All.SendAsync("UpdateTime", updateEvent);
        }

        internal void Resume()
        {
            timer.Start();
        }

        internal void Pause()
        {
            timer.Stop();
        }

        internal void Restart()
        {
            timer.Restart();
            timerFinished = false;
        }

        internal void SetDuration(TimeSpan timeSpan)
        {
            logger.LogInformation($"Got duration of {timeSpan}");
            duration = timeSpan;
            timer.Start();
        }
    }

    public class TimerEvent
    {
        public string Message { get; set; }
        public bool PauseEnabled { get; set; }
        public bool ResumeEnabled { get; set; }
        public bool RestartEnabled { get; set; }
    }
}