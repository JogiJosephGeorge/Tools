namespace PriceChecker.Common
{
    /// <summary>
    /// Extension class for Timer
    /// </summary>
    public class TimerExt
    {
        /// <summary>
        /// Action to be called periodically
        /// </summary>
        private Action action;

        /// <summary>
        /// Instance of Timer class
        /// </summary>
        private Timer timer;

        /// <summary>
        /// Create instance of TimerExt and start the timer.
        /// </summary>
        /// <param name="action">Action to be called periodically</param>
        /// <param name="dueTime">Waiting time to call the action for first time.</param>
        /// <param name="period">Waiting time between two calls.</param>
        public TimerExt(Action action, int dueTime, int period)
        {
            this.action = action;
            var callBack = new TimerCallback(CallUpdate);
            timer = new Timer(callBack, this, dueTime, period);
        }

        /// <summary>
        /// Static method used inside TimerCallback instance.
        /// </summary>
        /// <param name="obj">Instance of TimerExt class which is given while creating Timer.</param>
        private static void CallUpdate(object? obj)
        {
            if (obj is TimerExt timerExt)
            {
                ThreadControl.RunMainThread(timerExt.action);
            }
        }
    }
}
