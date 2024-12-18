using System;

namespace LoggerConverter.Models
{
    public class LogConverted
    {
        public int Id { get; set; }

        public int IdLog { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public string Path { get; set; }


        public virtual Log Log { get; set; }
    }
}