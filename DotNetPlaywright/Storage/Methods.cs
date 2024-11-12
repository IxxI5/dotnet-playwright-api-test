namespace DotNetPlaywright.Storage
{
    /// <summary>
    /// Storage Methods
    /// </summary>
    public class Method
    {
        /// <summary>
        /// SaveTokens Method to save tokens as Environment variables or directily write them to file
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="refreshToken"></param>
        public static void SaveTokens(string accessToken, string refreshToken)
        {
            // Store the tokens into environment variables in the current process scope only,
            // meaning they exist temporarily for the duration of the application’s runtime
            Environment.SetEnvironmentVariable("accessToken", accessToken);
            Environment.SetEnvironmentVariable("refreshToken", refreshToken);

            // Store the tokens into a .txt file permanently
            File.WriteAllText("tokens.txt", $"accessToken: {accessToken}\r refreshToken: {refreshToken}");
        }

        /// <summary>
        /// GetTokens Method to retrieve tokens from the Environment temporary variable
        /// </summary>
        /// <returns></returns>
        public static object GetTokens()
        {
            string? accessToken = Environment.GetEnvironmentVariable("accessToken");
            string? refreshToken = Environment.GetEnvironmentVariable("refreshToken");

            return new { AccessToken = accessToken, RefreshToken = refreshToken };
        }
    }
}
