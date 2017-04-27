using Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

namespace Repository
{
    public class TicketContext : DbContext
    {
        public TicketContext()
            : base(
                @"Data Source=(localdb)\v11.0;AttachDbFilename=|DataDirectory|\Repository.TicketContext.mdf;Initial Catalog=Repository.TicketContext;Integrated Security=True;MultipleActiveResultSets=True"
                )

        {
            Database.SetInitializer<TicketContext>(new TicketDbInitializer());
            this.Database.Log = new Action<string>(LogSQL);
        }

        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<TicketConversation> TicketConversations { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            //modelBuilder.Entity<Account>().HasKey(a => a.Email)
            //    .Property(a => a.Email).HasMaxLength(50);

            modelBuilder.Entity<Account>().Property(a => a.Password).IsRequired();
            modelBuilder.Entity<Account>().Property(a => a.UserName).IsRequired();
            modelBuilder.Entity<Ticket>().Property(a => a.TicketNumber).HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        public static void LogSQL(string sql)
        {
#if DEBUG
            Debug.Write(sql);
#endif
        }

    }

    public class TicketDbInitializer : DropCreateDatabaseIfModelChanges<TicketContext>
    {

        //public override void InitializeDatabase(TicketContext context)
        //{
        //    //This code avoid database drop problem when connection are open in Sql Server
        //    //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
        //   //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET MULTI_USER");
        //}


        protected override void Seed(TicketContext context)
        {
            //you can use ExecuteSqlCommand for excute adhoc sqlStament 
            //context.Database.ExecuteSqlCommand();
            //  context.Database.SqlCommand("ALTER DATABASE Tocrates SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

            var accounts = new List<Account>
            {
                new Account {ID=Guid.NewGuid(), UserName="admin", Email="maheshbailwal@gmail.com", Password ="MB248001",LastUpdateDate=DateTime.Now,CreateDate = DateTime.Now},
              //  new Account {UserName="user", Email="user@gmail.com", Password="1"}
            };
            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();
        }
    }



}
