using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    public class Message
    {
        public int Id { get; set; }
        public Nullable<int> SenderID { get; set; }
        public user Sender { get; set; }
        public DateTime date { get; set; }
        public Nullable<int> ConversationID { get; set; }
        public virtual Conversation Conversation { get; set; }
        public String Content { get; set; }
    }
}
