namespace PriceChecker.Logic.WebService
{
    /// <<summary>>
    /// Helper class to call HTTP methods
    /// <</summary>>
    public class HttpHelper
    {
        /// <summary>
        /// Instance of HttpClient
        /// </summary>
        private HttpClient Client { get; set; }

        /// <<summary>>
        /// Create instance of HttpHelper
        /// <</summary>>
        public HttpHelper()
        {
            Client = new HttpClient();
        }

        /// <summary>
        /// Make a GET request and return the response data.
        /// </summary>
        /// <param name="url">Full URL to be used to call GET method.</param>
        /// <returns>Response body of GET method.</returns>
        public string Get(string url)
        {
            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");

                //client.DefaultRequestHeaders.Accept
                //    .Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var auth = new Authentication();
                if (!string.IsNullOrWhiteSpace(auth.Key))
                {
                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {auth.Key}:{auth.Secret}");
                }

                var getTask = client.GetAsync(url);
                getTask.Wait();
                HttpResponseMessage response = getTask.Result;

                if (response.IsSuccessStatusCode)
                {
                    var readTask = response.Content.ReadAsStringAsync();
                    readTask.Wait();
                    string responseBody = readTask.Result;
                    return responseBody;
                }
            }

            return string.Empty;
        }
    }
}
