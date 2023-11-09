using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Net.Mail;

namespace BackgroundJobs.Services
{
    public class BackgroundJobsService : IBackgroundJobsService
    {
        private readonly AppDbContext appDbContext;
        public BackgroundJobsService(AppDbContext dbContext)
        {

            appDbContext = dbContext;
        }

        public async Task SendEmail()
        {
            Console.WriteLine("Job for sending emails fired!");
            var emails = await appDbContext.Emails.Where(x => x.Sent == null).ToListAsync();
            foreach (var email in emails)
            {
                var e = new MailMessage
                {
                    From = new MailAddress(email.From),
                    To = { email.To },
                    Subject = email.Subject,
                    Body = email.Body
                };

                var smtp = new SmtpClient("smtp.gmail.com")
                {
                    Port = 587,
                    Credentials = new NetworkCredential("th091605@gmail.com", "EnsarIbrahimovic123"),
                    EnableSsl = true,
                    UseDefaultCredentials = true
                };
                await smtp.SendMailAsync(e);
                email.Sent = DateTime.Now;
                

            }
            await appDbContext.SaveChangesAsync();
        }
    }
}