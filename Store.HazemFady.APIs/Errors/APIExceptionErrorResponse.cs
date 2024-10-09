namespace Store.HazemFady.APIs.Errors
{
    public class APIExceptionErrorResponse:APIErrorResponse
    {

        public string? Details { get; set; }

        public APIExceptionErrorResponse(int statusCode,string?message=null,string? details=null):base(statusCode,message)
        {
            Details = details;
        }
    }
}
