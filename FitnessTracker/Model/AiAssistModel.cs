using System.ComponentModel;

namespace FitnessTracker
{
    public class AiAssistModel : INotifyPropertyChanged
    {
        private string? image;
        private string? headerMessage;

        public string? Image
        {
            get { return image; }
            set
            {
                image = value;
                OnPropertyChanged("Image");
            }
        }

        public string? HeaderMessage
        {
            get { return headerMessage; }
            set
            {
                headerMessage = value;
                OnPropertyChanged("HeaderMessage");
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
