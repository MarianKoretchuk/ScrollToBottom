using System;
namespace ScrollToBottom
{
    public class ChatTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IncomingChatMessage { get; set; }
        public DataTemplate OutgoingChatMessage { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            var message = item as MessageModel;

            if (message == null)
            {
                return null;
            }

            if (message.IsIncomingMessage)
            {
                return IncomingChatMessage;
            }
            else
            {
                return OutgoingChatMessage;
            }
        }
    }
}

