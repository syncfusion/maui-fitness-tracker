using System.ComponentModel;

namespace FitnessTracker
{
    /// <summary>
    /// Represents the model used for AI assistant data binding. Implements INotifyPropertyChanged to notify clients of property changes.
    /// </summary>
    public class AiAssistModel : INotifyPropertyChanged
    {
        private string? image;
        private string? headerMessage;

        /// <summary>
        /// Gets or sets the image associated with the AI assistant. Notifies when the image changes.
        /// </summary>
        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        /// <summary>
        /// Gets or sets the header message for the AI assistant. Notifies when the message changes.
        /// </summary>
        public string? HeaderMessage
        {
            get { return headerMessage; }
            set
            {
                headerMessage = value;
                OnPropertyChanged(nameof(HeaderMessage));
            }
        }

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event for a given property name to notify subscribers.
        /// </summary>
        /// <param name="name">The name of the property that changed.</param>
        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
