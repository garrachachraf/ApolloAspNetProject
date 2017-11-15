using Apollo.Domain.entities;
using Apollo.ServicePattern;
using System;
using System.Linq;
using Apollo.Data.Infrastructures;

namespace Apollo.Services
{
    public class GestionConversation : Service<Conversation>, IGestionConversation
    {
        static DatabaseFactory dbFactory = new DatabaseFactory();
        static UnitOfWork utw = new UnitOfWork(dbFactory);

        public GestionConversation() : base(utw)
        {
        }

        public int CountAllMessages()
        {
            return 0;
        }

        public int CountAllUnreadMessages()
        {
            throw new NotImplementedException();
        }

        public int CountMessages(int ConversationID, int ClientID)
        {
            return utw.GetRepository<Message>().FindByCondition(x => x.ConversationID == ConversationID && x.SenderID== ClientID).Count();
        }

        public int CountUnreadConversations()
        {
            return utw.GetRepository<Conversation>().FindByCondition(x=>x.IsSeen == false).Count();
        }
        public bool ConversationsIsSeen(int ConversationID, int AdminID)
        {
            return utw.GetRepository<Message>().FindByCondition(x => x.ConversationID == ConversationID && x.SenderID != AdminID && x.seen == true).Count() >0;
        }

        public int CountUnreadMessages(int ConversationID)
        {
            throw new NotImplementedException();
        }

        public DateTime getLastMessageDate(int ConversationID)
        {
            return utw.GetRepository<Message>().FindByCondition(x => x.ConversationID == ConversationID).OrderByDescending(x=>x.date).First().date;
        }
    }
}
