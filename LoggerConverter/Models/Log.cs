using System;

namespace LoggerConverter.Models
{
    public class Log
    {
        public int Id { get; set; }

        public string Path { get; set; }

        public bool IsConverted { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }


        public virtual LogConverted LogConverted { get; set; }
    }
}