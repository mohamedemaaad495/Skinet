


namespace Api.Errors
{
    public class ApiResponse
    {
        public ApiResponse(int statusCode, string message = null)
        {
            StatusCode = statusCode;
            Message = message ?? GetDefaultStatusCode(statusCode);
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }

        private string GetDefaultStatusCode(int statusCode)
        {
            return statusCode switch
            {
                400 => "You Made A BadRequest!",
                401 => "You Are Not Authorized!",
                404 => "The Resource Is Not Found!",
                500 => "Error Are The Path Of The Dark Side. Errors Lead To Anger. Anger Leads To Hate. Hate Leads To Career Change",
                _ => null
            };
        }
    }
}
