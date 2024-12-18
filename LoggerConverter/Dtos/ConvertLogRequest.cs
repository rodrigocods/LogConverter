using System;

namespace LoggerConverter.Dtos
{
    public class ConvertLogRequest
    {
        public string LogFileUrl { get; set; }

        public int? IdLog { get; set; }

        public bool SaveOnDatabase { get; set; } = true;

        public void Validate()
        {
            if (string.IsNullOrWhiteSpace(LogFileUrl) && (IdLog == null || IdLog == default))
                throw new ArgumentException("É necessário informar a url do arquivo de log ou o id do log.");
        }
    }
}
