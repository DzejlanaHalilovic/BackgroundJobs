using Microsoft.AspNetCore.Mvc;
using Hangfire;
using BackgroundJobs.Models;

namespace BackgroundJobs.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly AppDbContext appDbContext;
        public WeatherForecastController(AppDbContext context)
        {
            appDbContext = context;
        }
      
       
        [HttpGet(Name = "FireHangfire")]
        public async Task<IActionResult> Get()
        {
            var email = new Email
            {

                From = "th091605@gmail.com",
                To = "nordingsoftversko@gmail.com",
                Subject = "Test",
                Body = "Test",
                Sent = DateTime.Now
                
            };
            appDbContext.Emails.Add(email);
            await appDbContext.SaveChangesAsync();
            return Ok();

        }
    }
}