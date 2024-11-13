using System.Text.Json;
using DotNetPlaywright.Model;
using DotNetPlaywright.Storage;
using Microsoft.Playwright;

namespace DotNetPlaywright
{
    /// <summary>
    /// API Tests using Playwright
    /// </summary>
    public class NUnit_ApiTests
    {
        private IPlaywright _playwright;
        private IBrowser _browser;
        private IAPIRequestContext _apiContext;

        private string? _accessToken;
        private string? _refreshToken;

        /// <summary>
        /// Launches Playwright for each Test
        /// </summary>
        /// <returns></returns>
        [SetUp]
        public async Task Setup()
        {
            _playwright = await Playwright.CreateAsync();
            _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = true });
            _apiContext = await _playwright.APIRequest.NewContextAsync();
        }

        [TearDown]
        public async Task Teardown()
        {
            await _apiContext.DisposeAsync();
            await _browser.DisposeAsync();
            _playwright.Dispose();
        }

        /// <summary>
        /// Login Test
        /// </summary>
        /// <returns>JSON object including the accessToken and refreshToken</returns>
        [Test, Order(1)]
        public async Task Login()
        {
            try
            {
                // API endpoint and payload
                var endpoint = "https://dummyjson.com/auth/login";
                var payload = new
                {
                    // login credentials
                    username = "emilys",
                    password = "emilyspass",
                    expiresInMins = 30
                };

                // POST request
                var response = await _apiContext.PostAsync(endpoint, new APIRequestContextOptions
                {
                    Headers = new Dictionary<string, string> { { "Content-Type", "application/json" } },
                    DataObject = payload
                });

                // if response NOT successful
                if (!response.Ok)
                {
                    throw new HttpRequestException($"API request failed with status code: {response.Status}");
                }

                // Parse JSON response into the ApiResponse model
                var responseBody = await response.TextAsync();
                var apiResponse = JsonSerializer.Deserialize<ApiResponse>(responseBody, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });


                if (apiResponse == null)
                {
                    throw new JsonException("Failed to parse JSON response.");
                }

                // Print the raw JSON response to the console
                Console.WriteLine("Raw JSON response:");
                Console.WriteLine(responseBody);

                // Extract tokens from the response
                _accessToken = apiResponse.AccessToken;
                _refreshToken = apiResponse.RefreshToken;

                // Assert tokens are valid
                Assert.IsNotNull(_accessToken, "Access token should not be null.");
                Assert.IsNotNull(_refreshToken, "Refresh token should not be null.");

                // Save tokens for further use
                Method.SaveTokens(_accessToken, _refreshToken);
            }
            catch (HttpRequestException ex)
            {
                Assert.Fail($"HTTP request failed: {ex.Message}");
            }
            catch (JsonException ex)
            {
                Assert.Fail($"Failed to parse JSON: {ex.Message}");
            }
            catch (Exception ex)
            {
                Assert.Fail($"An unexpected error occurred: {ex.Message}");
            }
        }

        /// <summary>
        /// Get Current Auhenticated User
        /// </summary>
        /// <returns>JSON object including user's firstName, lastName</returns>
        [Test, Order(2)]
        public async Task GetCurrentUser()
        {
            try
            {
                // API endpoint and payload
                var endpoint = "https://dummyjson.com/auth/me";

                if (_accessToken == null)
                {
                    throw new JsonException("Access Token is null.");
                }

                // GET request
                var response = await _apiContext.GetAsync(endpoint, new APIRequestContextOptions
                {
                    Headers = new Dictionary<string, string> { { "Authorization", $"Bearer {_accessToken}" } },
                    
                });

                // Parse JSON response into the ApiResponse model
                var responseBody = await response.TextAsync();

                // Print the raw JSON response to the console
                Console.WriteLine("Raw JSON response:");
                Console.WriteLine(responseBody);
            }
            catch (JsonException)
            {
                Assert.Fail("Access Token is null.");
            }
            catch (Exception ex)
            {
                Assert.Fail("An unexpected Error occured:", ex.Message);
            }
        }

        [Test, Order(3)]
        public void GetStoredTokensTest()
        {
            // Assign a dynamic variable for the GetTokens object 
            dynamic tokens = Method.GetTokens();

            // Extract Access and Refresh tokens from the GetTokens object
            string accessToken = tokens.AccessToken;
            string refreshToken = tokens.RefreshToken;

            // Display the tokens in console
            Console.WriteLine($"AccessToken: {accessToken}");
            Console.WriteLine($"RefreshToken: {refreshToken}");
        }
    }
}
