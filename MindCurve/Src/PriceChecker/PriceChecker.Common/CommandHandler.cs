using System.Windows.Input;

namespace PriceChecker.Common
{
    /// <summary>
    /// Defines a common command
    /// </summary>
    public class CommandHandler : ICommand
    {
        /// <summary>
        /// Action to be executed by the command.
        /// </summary>
        private Action ExecuteHandler;

        /// <summary>
        /// Creates instance of the command handler
        /// </summary>
        /// <param name="executeHandler">Action to be executed by the command</param>
        public CommandHandler(Action executeHandler)
        {
            ExecuteHandler = executeHandler;
        }

        /// <summary>
        /// Wires CanExecuteChanged event 
        /// </summary>
        public event EventHandler? CanExecuteChanged;

        /// <summary>
        /// Defines the method that determines whether the command can execute
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.</param>
        /// <returns>true if this command can be executed; otherwise, false.</returns>
        public bool CanExecute(object? parameter)
        {
            return true;
        }

        /// <summary>
        /// Defines the method to be called when the command is invoked.
        /// </summary>
        /// <param name="parameter">Data used by the command. If the command does not require data to be passed, 
        /// this object can be set to null.</param>
        public void Execute(object? parameter)
        {
            if (ExecuteHandler != null)
            {
                ThreadControl.RunMainThread(delegate
                {
                    ExecuteHandler();
                });
            }
        }
    }
}
