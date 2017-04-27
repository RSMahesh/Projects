// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TicketContext.cs" company="">
//   
// </copyright>
// <summary>
//   The ticket context.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Xml;

using Entities;

namespace Repository
{
    using Test.Api.Data.Entities;

    /// <summary>
    /// The ticket context.
    /// </summary>
    public class TicketContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TicketContext"/> class.
        /// </summary>
        public TicketContext()
            : base(
                @"Data Source=(localdb)\v11.0;AttachDbFilename=|DataDirectory|\Repository.TicketContext.mdf;Initial Catalog=Repository.TicketContext;Integrated Security=True;MultipleActiveResultSets=True"
                )
        {
            Database.SetInitializer<TicketContext>(new TicketDbInitializer());
            this.Database.Log = new Action<string>(LogSQL);
        }

        /// <summary>
        /// Gets or sets the tickets.
        /// </summary>
        public DbSet<TicketEntity> Tickets { get; set; }

        /// <summary>
        /// Gets or sets the ticket conversations.
        /// </summary>
        public DbSet<TicketConversationEntity> TicketConversations { get; set; }

        /// <summary>
        /// Gets or sets the accounts.
        /// </summary>
        public DbSet<UserEntity> Accounts { get; set; }

        /// <summary>
        /// Gets or sets the user role.
        /// </summary>
        public DbSet<UserRoleEntity> UserRole { get; set; }

        /// <summary>
        /// Gets or sets the acl.
        /// </summary>
        public DbSet<ACLEntity> Acl { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // modelBuilder.Entity<Account>().HasKey(a => a.Email)
            // .Property(a => a.Email).HasMaxLength(50);

            // modelBuilder.Entity<User>().Property(a => a.Password).IsRequired();
            // modelBuilder.Entity<User>().Property(a => a.Email).HasMaxLength(50);

            // modelBuilder.Entity<User>().Property(a => a.UserName).IsRequired();
            modelBuilder.Entity<TicketEntity>()
                .Property(a => a.TicketNumber)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }

        /// <summary>
        /// The log sql.
        /// </summary>
        /// <param name="sql">
        /// The sql.
        /// </param>
        public static void LogSQL(string sql)
        {
#if DEBUG
            Debug.Write(sql);
#endif
        }
    }

    /// <summary>
    /// The ticket db initializer.
    /// </summary>
    public class TicketDbInitializer : DropCreateDatabaseIfModelChanges<TicketContext>
    {
        // public override void InitializeDatabase(TicketContext context)
        // {
        // //This code avoid database drop problem when connection are open in Sql Server
        // //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
        // //context.Database.ExecuteSqlCommand(TransactionalBehavior.DoNotEnsureTransaction, "ALTER DATABASE [" + context.Database.Connection.Database + "] SET MULTI_USER");
        // }

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(TicketContext context)
        {
            // you can use ExecuteSqlCommand for excute adhoc sqlStament 
            // context.Database.ExecuteSqlCommand();
            // context.Database.SqlCommand("ALTER DATABASE Tocrates SET SINGLE_USER WITH ROLLBACK IMMEDIATE");

           UserRoleEntity userRole = new UserRoleEntity();
            userRole.OrgUnitId = Guid.Empty;
            userRole.ID = Guid.NewGuid();
            userRole.Description = "Admin";
            context.UserRole.Add(userRole);

         ACLEntity acl = new ACLEntity();
            acl.ID = Guid.NewGuid();
            acl.SourceID = userRole.ID;
          
            //SecurableObjectType.Organization
            acl.SecurableObjectType = 1;
            acl.Permissions = ulong.MaxValue;
            context.Acl.Add(acl);

            var accounts = new List<UserEntity>
                               {
                                   // new User {ID=Guid.NewGuid(), UserName="admin", Email="maheshbailwal@gmail.com", Password ="MB248001",LastUpdateDate=DateTime.Now,CreateDate = DateTime.Now},
                                   new UserEntity()
                                       {
                                           Id = Guid.NewGuid(), 
                                           UserName = "maheshbailwal@gmail.com", 
                                           EmailAddress = "maheshbailwal@gmail.com", 
                                           Password = "MB248001", 
                                           AdditionalMessage = "First User",
                                           RoleId = userRole.ID
                                       }
                               };
            accounts.ForEach(a => context.Accounts.Add(a));
            context.SaveChanges();
        }
    }
}