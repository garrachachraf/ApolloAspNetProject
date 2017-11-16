using Apollo.Domain.entities;
using Apollo.ServicePattern;
using System;

namespace Apollo.Services
{
    public interface IGestionConversation : IService<Conversation>
    {
        DateTime getLastMessageDate(int ConversationID);
        int CountMessages(int ConversationID, int AdminID);
        int CountAllMessages();
        int CountUnreadMessages(int ConversationID);
        int CountAllUnreadMessages();
        int CountUnreadConversations();

        
    }
}