using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Input;
using Syncfusion.Maui.AIAssistView;

#nullable disable
namespace FitnessTracker
{
    public class AiAssistViewModel : INotifyPropertyChanged
    {
        #region Field
        ObservableCollection<IAssistItem> messages;
        ObservableCollection<AiAssistModel> headerInfoCollection;
        List<List<string>> suggestionlist = new List<List<string>>();
        //AzureAIService azureAIService;
        Thickness headerPadding;
        Thickness editorPadding;
        internal double editorBottomPadding = 0.0;
        bool cancelResponse;
        bool enableSendIcon;
        bool isHeaderVisible = true;
        internal bool enableNewChatIcon = true;
        bool isAttachmentPopupOpen;
        string inputText;
        IAssistItem requestItem;
        bool isNewChatViewVisible = false;
        bool canAddResponse = true;

        #endregion

        #region Constructor
        public AiAssistViewModel()
        {
           // azureAIService = new AzureAIService();
            GetHeaderInfo();
            GenerateSuggestions();
            messages = new ObservableCollection<IAssistItem>();
            NewChatTappedCommand = new Command<object>(ExecuteNewChatTappedCommand);
            SendButtonCommand = new Command(ExecuteSendButtonCommand);
            ChipCommand = new Command<object>(ExecuteChipCommand);
            CopyCommand = new Command<object>(ExecuteCopyCommand);
            RetryCommand = new Command<object>(ExecuteRetryCommand);
            AssistViewRequestCommand = new Command<object>(ExecuteRequestCommand);
            HeaderItemTappedCommand = new Command(HeaderItemTapCommand);
            StopRespondingCommand = new Command(ExecuteStopResponding);
            AttachmentButtonCommand = new Command(ShowAttachmentPopup);
        }

        #endregion

        #region Private Properties

        /// <summary>
        /// Stores a collection of header messages displayed in the AI assistant.
        /// </summary>
        ObservableCollection<string> HeaderMessages { get; set; } = new ObservableCollection<string>
        {
            "Text",
            "Text",
        };

        /// <summary>
        /// Stores a collection of image file paths used in the AI assistant.
        /// </summary>
        ObservableCollection<string> ImagesCollection { get; set; } = new ObservableCollection<string>
        {
            "texticon.png",
            "texticon.png",
        };

        #endregion

        #region Commands

        public ICommand CopyCommand { get; set; }
        public ICommand RetryCommand { get; set; }
        public ICommand AssistViewRequestCommand { get; set; }
        public ICommand HeaderItemTappedCommand { get; set; }
        public ICommand StopRespondingCommand { get; set; }
        public ICommand SendButtonCommand { get; set; }
        public ICommand ChipCommand { get; set; }
        public ICommand NewChatTappedCommand { get; set; }
        public ICommand AttachmentButtonCommand { get; set; }
        #endregion

        #region Public Properties

        /// <summary>
        /// Represents a collection of header information for AI assistance.
        /// </summary>
        public ObservableCollection<AiAssistModel> HeaderInfoCollection
        {
            get
            {
                return headerInfoCollection;
            }
            set
            {
                headerInfoCollection = value;
            }
        }

        /// <summary>
        /// Stores chat messages in the AI assistant.
        /// </summary>
        public ObservableCollection<IAssistItem> Messages
        {
            get
            {
                return messages;
            }

            set
            {
                messages = value;
                RaisePropertyChanged(nameof(Messages));
            }
        }

        /// <summary>
        /// Indicates whether the AI response should be canceled.
        /// </summary>
        public bool CancelResponse
        {
            get
            {
                return cancelResponse;
            }
            set
            {
                cancelResponse = value;
                RaisePropertyChanged(nameof(CancelResponse));
            }
        }

        /// <summary>
        /// Determines the visibility of the header section.
        /// </summary>
        public bool IsHeaderVisible
        {
            get
            {
                return isHeaderVisible;
            }
            set
            {
                isHeaderVisible = value;
                RaisePropertyChanged(nameof(IsHeaderVisible));
            }
        }

        /// <summary>
        /// Indicates whether the new chat view is visible.
        /// </summary>
        public bool IsNewChatViewVisible
        {
            get
            {
                return isNewChatViewVisible;
            }
            set
            {
                isNewChatViewVisible = value;
                RaisePropertyChanged(nameof(IsNewChatViewVisible));
            }
        }

        /// <summary>
        /// Enables or disables the send icon based on input availability.
        /// </summary>
        public bool EnableSendIcon
        {
            get
            {
                return enableSendIcon;
            }
            set
            {
                enableSendIcon = value;
                RaisePropertyChanged(nameof(EnableSendIcon));
            }
        }

        /// <summary>
        /// Indicates whether the attachment popup is open.
        /// </summary>
        public bool IsAttachmentPopupOpen
        {
            get
            {
                return isAttachmentPopupOpen;
            }
            set
            {

                isAttachmentPopupOpen = value;
                RaisePropertyChanged(nameof(IsAttachmentPopupOpen));
            }
        }

        /// <summary>
        /// Gets or sets the padding for the header section.
        /// </summary>
        public Thickness HeaderPadding
        {
            get { return headerPadding; }
            set { headerPadding = value; RaisePropertyChanged(nameof(HeaderPadding)); }
        }

        /// <summary>
        /// Gets or set the padding for the input editor.
        /// </summary>
        public Thickness EditorPadding
        {
            get { return editorPadding; }
            set { editorPadding = value; RaisePropertyChanged(nameof(EditorPadding)); }
        }

        /// <summary>
        /// Represents the user's input text in the chat.
        /// </summary>
        public string InputText
        {
            get { return inputText; }
            set { inputText = value; RaisePropertyChanged(nameof(InputText)); }
        }

        #endregion

        #region Internal Methods

        internal async Task GetResult(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var userAIPrompt = GetUserAIPrompt(request.Text);
                var response = /*await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true)*/ "";
                response = response.Replace("\n", "<br>");
                if (!CancelResponse && canAddResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response };
                    responseItem.RequestItem = inputQuery;
                    Messages.Add(responseItem);
                    enableNewChatIcon = true;
                    IsNewChatViewVisible = true;
                    EnableSendIcon = !string.IsNullOrEmpty(InputText);
                }
            }

            CancelResponse = false;
        }
        #endregion

        #region Private Methods

        async void ExecuteNewChatTappedCommand(object obj)
        {
            await Task.Delay(100);
            canAddResponse = false;
            IsHeaderVisible = true;
            IsNewChatViewVisible = false;
            enableNewChatIcon = true;
            EditorPadding = new Thickness(17, 0, 16, editorBottomPadding);
            InputText = string.Empty;
            Messages.Clear();
        }
        void ShowAttachmentPopup()
        {
            IsAttachmentPopupOpen = true;
        }
        async void HeaderItemTapCommand(object obj)
        {
            requestItem = new AssistItem() { Text = (obj as Label).Text, IsRequested = true };
            Messages.Add(requestItem);
            await GetResponseWithSuggestion(requestItem).ConfigureAwait(true);
        }

        async void ExecuteRequestCommand(object obj)
        {
            requestItem = (obj as Syncfusion.Maui.AIAssistView.RequestEventArgs).RequestItem;
            await GetResult(requestItem).ConfigureAwait(true);
        }

        void ExecuteCopyCommand(object obj)
        {
            string text = (obj as AssistItem).Text;
            text = Regex.Replace(text, "<.*?>|&nbsp;", string.Empty);
            Clipboard.SetTextAsync(text);
        }

        async void ExecuteRetryCommand(object obj)
        {
            enableNewChatIcon = false;
            IsNewChatViewVisible = true;
            IAssistItem item = (obj as AssistItem).RequestItem as IAssistItem;
            if (item != null)
            {
                requestItem = item;
            }

            await GetResult(requestItem).ConfigureAwait(true);
        }

        void ExecuteChipCommand(object obj)
        {
            var chipText = obj as string;

            if (chipText == "Ownership")
            {
                InputText = "Characteristics of Ownership";
            }
            else if (chipText == "Listening")
            {
                InputText = "Types of Listening";
            }
            else
            {
                InputText = chipText;
            }
        }

        async void ExecuteSendButtonCommand()
        {
            await Task.Delay(100);
            enableNewChatIcon = false;
            canAddResponse = true;
            IsNewChatViewVisible = true;
            var text = InputText;
            IsHeaderVisible = false;
            if (DeviceInfo.Platform == DevicePlatform.Android || DeviceInfo.Platform == DevicePlatform.iOS)
            {
                EditorPadding = new Thickness(17, 8, 16, 24);
            }
            else
            {
                EditorPadding = new Thickness(17, 8, 16, 16);
            }

            requestItem = new AssistItem()
            {
                Text = text,
                IsRequested = true
            };

            await Task.Delay(2);
            Messages.Add(requestItem);
            InputText = string.Empty;
            await GetResult(requestItem);
        }

        void ExecuteStopResponding()
        {
            CancelResponse = true;
            AssistItem responseItem = new AssistItem() { Text = "You canceled the response" };
            responseItem.ShowAssistItemFooter = false;
            Messages.Add(responseItem);
            IsNewChatViewVisible = true;
            enableNewChatIcon = true;
            EnableSendIcon = !string.IsNullOrEmpty(InputText);
        }

        void GetHeaderInfo()
        {
            var headerInfo = new ObservableCollection<AiAssistModel>();
            for (int i = 0; i < 2; i++)
            {
                var gallery = new AiAssistModel()
                {
                    Image = ImagesCollection[i],
                    HeaderMessage = HeaderMessages[i],
                };

                headerInfo.Add(gallery);
            }

            headerInfoCollection = headerInfo;
        }

        string GetUserAIPrompt(string userPrompt)
        {
            string userQuery = $"Given User query: {userPrompt}." +
                               $"\nSome conditions need to follow:" +
                               $"\nGive heading of the topic and simplified answer in 4 points with numbered format" +
                               $"\nGive as string alone" +
                               $"\nRemove ** and remove quotes if it is there in the string.";

            return userQuery;
        }

        async Task GetResponseWithSuggestion(object inputQuery)
        {
            await Task.Delay(3000).ConfigureAwait(true);
            AssistItem request = (AssistItem)inputQuery;
            if (request != null)
            {
                var userAIPrompt = GetUserAIPrompt(request.Text);
                var response = /*await azureAIService!.GetResultsFromAI(request.Text, userAIPrompt).ConfigureAwait(true)*/"";
                response = response.Replace("\n", "<br>");
                await Task.Delay(1000).ConfigureAwait(true);
                var suggestion = GetSuggestion(request.Text);
                await Task.Delay(1000).ConfigureAwait(true);
                if (!CancelResponse)
                {
                    AssistItem responseItem = new AssistItem() { Text = response, Suggestion = suggestion };
                    responseItem.RequestItem = inputQuery;
                    Messages.Add(responseItem);
                }
            }

            CancelResponse = false;
        }

        void GenerateSuggestions()
        {
            List<string> firstHeaderSuggestion = new List<string> { "Initiative", "Responsibility", "Accountability" };
            List<string> secondHeaderSuggestion = new List<string> { "Different Perspective", "More Ideas" };
            List<string> thirdHeaderSuggestion = new List<string> { "Active Listening", "Passive Listening" };
            suggestionlist.Add(firstHeaderSuggestion);
            suggestionlist.Add(secondHeaderSuggestion);
            suggestionlist.Add(thirdHeaderSuggestion);
        }

        AssistItemSuggestion GetSuggestion(string prompt)
        {
            var promptSuggestions = new AssistItemSuggestion();

            for (int i = 0; i < HeaderMessages.Count() - 1; i++)
            {
                if (HeaderMessages[i].Contains(prompt))
                {
                    var suggestions = new ObservableCollection<ISuggestion>();
                    foreach (var items in suggestionlist[i])
                    {
                        suggestions.Add(new AssistSuggestion() { Text = items });
                    }

                    promptSuggestions.Items = suggestions;
                    promptSuggestions.Orientation = SuggestionsOrientation.Horizontal;
                    return promptSuggestions;
                }
            }

            return promptSuggestions;
        }

        #endregion

        #region PropertyChanged

        /// <summary>
        /// Property changed handler.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when property is changed.
        /// </summary>
        /// <param name="propName">changed property name</param>
        public void RaisePropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        #endregion
    }
}
