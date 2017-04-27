using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using EntityCodeFirst.Entities;
namespace EntityCodeFirst
{
   public class TicketContext: DbContext
    {
            public DbSet<Ticket> Tickets { get; set; }
            public DbSet<TicketConversation> TicketConversations { get; set; }
    }
}
