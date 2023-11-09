﻿namespace BackgroundJobs.Models
{
    public class Email

    {
        public int Id { get; set; }
        public string? To { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public string? From { get; set; }
        public DateTime Sent { get; set; }
    }
}
