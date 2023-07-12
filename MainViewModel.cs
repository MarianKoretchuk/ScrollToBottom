using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace ScrollToBottom
{
    public class MessageModel
    {
        public bool IsIncomingMessage { get; set; }

        public string Message { get; set; }

        public string Url { get; set; }
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<MessageModel> _messages;

        public ObservableCollection<MessageModel> Messages
        {
            get => _messages;
            set
            {
                if (_messages != value)
                {
                    _messages = value;
                    OnPropertyChanged();
                }
            }
        }

        public ICommand AddMessageCommad => new Command(() =>
        {
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                Messages.Add(new MessageModel { IsIncomingMessage = false, Message = "Some text" });
            });
        });

        public ICommand AddImageCommad => new Command(() =>
        {
            Application.Current.Dispatcher.Dispatch(async () =>
            {
                Messages.Add(new MessageModel { IsIncomingMessage = false, Url = "dotnet_bot" });
            });
        });

        public MainViewModel()
        {
            Messages = new ObservableCollection<MessageModel>();

            Application.Current.Dispatcher.Dispatch(() =>
            {
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });

                Messages.Add(new MessageModel { IsIncomingMessage = true, Url = "dotnet_bot" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = false, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = false, Url = "dotnet_bot" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
                Messages.Add(new MessageModel { IsIncomingMessage = true, Message = "AA" });
            });

        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

