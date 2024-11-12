namespace DotNetPlaywright.Model
{
    /// <summary>
    /// ApiResponse Model to match the JSON structure of the API response
    /// </summary>
    public class ApiResponse
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public string Username { get; set; }
    }
}
