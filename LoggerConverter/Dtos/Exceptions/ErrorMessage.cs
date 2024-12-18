using LoggerConverter.Dtos.Exceptions.Enums;

namespace LoggerConverter.Dtos.Exceptions
{
    public class ErrorResponse
    {
        public ErrorResponse(string message, string details = "", int statusCode = 406,
            string type = ErrorMessageTypeEnum.WARNING)
        {
            Message = message;
            Details = details;
            StatusCode = statusCode;
            Type = type;
        }

        public string Message { get; set; }
        public string Details { get; set; }
        public int StatusCode { get; set; }
        public string Type { get; set; }
    }
}
