namespace PriceChecker.Logic.WebService
{
    internal class Authentication
    {
        /// <summary>
        /// API public key for authentication
        /// </summary>
        public string Key { get; set; } = ""; // Provide API Key for authentication

        /// <summary>
        /// API Secret key for authentication
        /// </summary>
        public string Secret { get; set; } = ""; // Provide API Secret for authentication
    }
}
