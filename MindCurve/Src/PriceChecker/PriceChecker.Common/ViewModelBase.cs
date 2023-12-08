using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PriceChecker.Common
{
    /// <summary>
    /// Base class of all view model classes
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// This event will be triggered when a property is changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Method to be called when a property is changed
        /// </summary>
        /// <param name="propertyName">Name of the property changed.</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
