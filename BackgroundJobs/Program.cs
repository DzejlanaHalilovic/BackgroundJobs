using BackgroundJobs;
using BackgroundJobs.Services;
using Hangfire;
using Hangfire.Storage.SQLite;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer("server=(LocalDB)\\MSSQLLocalDB;database=backgroundjobsproject;Integrated Security=True;encrypt=false");
});
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHangfire(configuration =>
{
    configuration.UseSQLiteStorage("hangfire.db");
    configuration.UseSimpleAssemblyNameTypeSerializer();
    configuration.UseRecommendedSerializerSettings();
});

builder.Services.AddHangfireServer();
//za svaki servis pravi novu instancu klase
builder.Services.AddTransient<IBackgroundJobsService, BackgroundJobsService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireServer();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<IBackgroundJobsService>(x => x.SendEmail(), Cron.Minutely);

app.Run();