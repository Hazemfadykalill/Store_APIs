namespace Store.HazemFady.APIs.Errors
{
    public class APIErrorResponse
    {

        public int StatusCode { get; set; }
        public string? MessageResponse { get; set; }

        public APIErrorResponse(int statusCode, string? messageResponse=null)
        {
            StatusCode = statusCode;
            MessageResponse = messageResponse??getMessageBasedOnStatusCode(statusCode);
        }
        //to generate message Response Based On StatusCode 
        private string getMessageBasedOnStatusCode(int statusCode)
        {

            var message = statusCode switch
            {
                400 => "A Bad Request ,You Have Made",
                401 => "Authorized You AreNot ",
                404 => "Data Not Found",
                500 => "A Server Error",
                _ => null,
            };

            return message!;
        }
    }
}
