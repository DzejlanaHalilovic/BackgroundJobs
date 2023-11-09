namespace BackgroundJobs.Services
{
    public interface IBackgroundJobsService
    {
        Task SendEmail();
    }
}