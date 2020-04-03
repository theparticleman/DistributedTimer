using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DistributedTimer.Controllers
{
    public class TimerController : Controller
    {
        private readonly Timer timer;
        private readonly ILogger<TimerController> logger;

        public TimerController(Timer timer, ILogger<TimerController> logger)
        {
            this.timer = timer;
            this.logger = logger;
        }

        [Route("/timer/pause")]
        public void Pause()
        {
            timer.Pause();
        }

        [Route("/timer/resume")]
        public void Resume()
        {
            timer.Resume();
        }
    }
}