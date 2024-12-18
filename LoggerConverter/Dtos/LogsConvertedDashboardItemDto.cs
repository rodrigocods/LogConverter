using LoggerConverter.Models;

namespace LoggerConverter.Dtos
{
    public class LogsConvertedDashboardItemDto
    {
        public Log Log { get; set; }

        public LogConverted LogConverted { get; set; }
    }
}