using System;
using DistributedTimer.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace DistributedTimer
{
    public class Timer
    {
        private readonly IHubContext<TimerHub> timerHub;
        private DateTime endTime;

        public Timer(IHubContext<TimerHub> timerHub)
        {
            this.timerHub = timerHub;
        }

        internal void Execute()
        {
            if (endTime == default)
            {
                timerHub.Clients.All.SendAsync("UpdateTime", "waiting for timer to start");
                return;
            }
            
            var remainingTime = endTime - DateTime.Now;
            string result = remainingTime.TotalMilliseconds > 0 ? remainingTime.ToString(@"mm\:ss") : "00:00";
            timerHub.Clients.All.SendAsync("UpdateTime", result);
        }

        internal void SetDuration(TimeSpan timeSpan)
        {
            endTime = DateTime.Now + timeSpan;
        }
    }
}