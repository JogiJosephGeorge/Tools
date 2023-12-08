namespace PriceChecker.Common
{
    /// <summary>
    /// Logger class to handle logging
    /// </summary>
    public class Logger
    {
        /// <summary>
        /// Single instance of Logger class
        /// </summary>
        private static readonly Logger Instance = new Logger();

        /// <summary>
        /// Event action of all subscribed log events
        /// </summary>
        private event Action<string>? LogEvent;

        /// <summary>
        /// Create instance of Logger class privately
        /// </summary>
        private Logger()
        {
        }

        /// <summary>
        /// Add the given action to the log event.
        /// </summary>
        /// <param name="action">Action to be added to the log event.</param>
        public static void AddAction(Action<string> action)
        {
            Instance.LogEvent += action;
        }

        /// <summary>
        /// Remove the given action to the log event.
        /// </summary>
        /// <param name="action">Action to be removed from log event.</param>
        public static void RemoveAction(Action<string> action)
        {
            Instance.LogEvent -= action;
        }

        /// <summary>
        /// Logs the given message
        /// </summary>
        /// <param name="message">String message to be logged</param>
        public static void Log(string message)
        {
            Instance.LogEvent?.Invoke(message);
        }
    }
}
