namespace Core.Results
{
    public class ErrorResponse(int statusCode, IList<string> errors)
    {
        public int Status { get; set; } = statusCode;
        public IList<string> Errors { get; set; } = errors;
    }
}
