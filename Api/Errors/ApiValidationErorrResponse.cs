namespace Api.Errors
{
    public class ApiValidationErorrResponse : ApiResponse
    {
        public ApiValidationErorrResponse() : base(400)
        {
        }
        public IEnumerable<string> Errors { get; set; }
    }
}
