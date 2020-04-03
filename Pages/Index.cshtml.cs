using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DistributedTimer.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> logger;
        private readonly Timer timer;

        public IndexModel(ILogger<IndexModel> logger, Timer timer)
        {
            this.logger = logger;
            this.timer = timer;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost(int value)
        {
            logger.LogInformation("Got timer value of " + value);
            timer.SetDuration(TimeSpan.FromMinutes(value));
            return new RedirectToPageResult("Timer");
        }
    }
}
