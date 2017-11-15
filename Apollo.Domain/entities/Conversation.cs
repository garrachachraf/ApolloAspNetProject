using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apollo.Domain.entities
{
    public class Conversation
    {
        public int Id { get; set; }
        public virtual user Client { get; set; }
        public Nullable<int> ClientID { get; set; }
        public Nullable<int> AdminID { get; set; }
        public user Admin { get; set; }
        public string Object { get; set; }
        public virtual ICollection<Message> Messages { get; set; }
    }
}
