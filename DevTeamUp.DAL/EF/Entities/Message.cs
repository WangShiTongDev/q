using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevTeamUp.DAL.EF.Entities
{
    public class Message : EntityBase
    {
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public int SenderId { get; set; }
        public User Sender { get; set; }
        public int ChatId { get; set; }
        public ProjectChat Chat { get; set; }
    }
}
