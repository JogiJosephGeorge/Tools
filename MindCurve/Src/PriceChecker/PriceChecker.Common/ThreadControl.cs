using System.Windows;

namespace PriceChecker.Common
{
    /// <summary>
    /// Utility class for handling thread operations
    /// </summary>
    public static class ThreadControl
    {
        /// <summary>
        /// Specifies that the current thread is UI thread or not.
        /// </summary>
        public static bool IsMainThread
        {
            get
            {
                if (Application.Current != null)
                {
                    return Application.Current.Dispatcher.CheckAccess();
                }

                return false;
            }
        }

        /// <summary>
        /// Run the given action on UI thread (main thread)
        /// </summary>
        /// <param name="method">Action to be executed on UI thread.</param>
        public static void RunMainThread(Action method)
        {
            if (IsMainThread)
            {
                method();
            }
            else
            {
                Application.Current.Dispatcher.Invoke(method);
            }
        }

    }
}
