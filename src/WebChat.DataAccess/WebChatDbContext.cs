namespace WebChat.DataAccess
{
    #region Using

    using System;
    using System.Collections.Generic;
    using System.Data.Common;
    using System.Globalization;
    using System.Linq;
    using Microsoft.AspNet.Identity.EntityFramework;
    using WebChat.Models.Entities.Identity;

    #endregion

    public class WebChatDbContext : IdentityDbContext<AppUser,
                                                      AppRole,
                                                      long,
                                                      AppUserLogin,
                                                      AppUserRole,
                                                      AppUserClaim>
    {
        public static DbConnection dbConnection;

        public static string connectionString;       
        private WebChatDbContext() : base("WebChatDbContext")
        {
        }
        private WebChatDbContext(DbConnection dbConnection) : base(dbConnection, contextOwnsConnection: true)
        {
        }
        private WebChatDbContext(string connectionString) : base(connectionString)
        {
        }

        public static WebChatDbContext GetInstance()
        {
            if (dbConnection != null)
                return new WebChatDbContext(dbConnection);
            else if (!string.IsNullOrEmpty(connectionString))
                return new WebChatDbContext(connectionString);
            else
                return new WebChatDbContext();
        }

        public string GenerateCustomerAppKey()
        {
            return Database.SqlQuery<string>("SELECT dbo.GenerateAppKey()").FirstOrDefault();
        }

        #region Tables
        public virtual DbSet<AppUserRole> UserRoles { get; set; }
        public virtual DbSet<CustomerApplication> CustomerApplication { get; set; }
        public virtual DbSet<Dialog> Dialog { get; set; }
        public virtual DbSet<Message> Message { get; set; }

        #endregion

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<AppRole>().ToTable("Role");
            builder.Entity<AppUserClaim>().ToTable("UserClaim");
            builder.Entity<AppUserRole>().ToTable("UserRole");
            builder.Entity<AppUserLogin>().ToTable("UserLogin");

 /*------------------------------------------ User Table -------------------------------------------*/
            var userTable = builder.Entity<AppUser>().ToTable("User");
            userTable.Property(user => user.Email)
                .IsOptional()
                .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("EmailUniqueIndex") { IsUnique = true }));

            userTable.HasMany(user => user.Dialogs)
                     .WithMany(dialog => dialog.Users)
                     .Map(ud =>
                     {
                         ud.ToTable("UserDialog");
                         ud.MapLeftKey("User_Id");
                         ud.MapRightKey("Dialog_Id");
                     });
            userTable.Property(user => user.RegistrationDate).IsOptional();

            userTable.HasMany(user => user.RelatedApplications)
                     .WithMany(app => app.RelatedUsers)
                     .Map(user_app =>
                     {
                         user_app.ToTable("UserApp");
                         user_app.MapLeftKey("UserId");
                         user_app.MapRightKey("AppId");
                     });

/*-------------------------------- Customer Application Table ------------------------------------*/

            builder.Entity<CustomerApplication>().ToTable("CustomerApplication")
                .HasKey<int>(app => app.Id)
                .HasMany(app => app.RelatedUsers)
                     .WithMany(user => user.RelatedApplications)
                     .Map(user_app =>
                     {
                         user_app.ToTable("UserApp");
                         user_app.MapLeftKey("AppId");
                         user_app.MapRightKey("UserId");
                     });

            builder.Entity<CustomerApplication>()
                .HasRequired(app => app.Owner)
                    .WithMany(user => user.myOwnApplications)
                    .HasForeignKey(app => app.OwnerUser_Id);

            builder.Entity<CustomerApplication>()
                .Property(app => app.ContactEmail).IsRequired();

            builder.Entity<CustomerApplication>()
                .Property(app => app.WebsiteUrl).IsRequired();

/*--------------------------------------- Dialog Table --------------------------------------------*/
            builder.Entity<Dialog>().ToTable("Dialog");
            builder.Entity<Dialog>()
                .HasKey<int>(dialog => dialog.Id)
                .HasMany(dialog => dialog.Messages)
                    .WithRequired(message => message.Dialog)
                    .HasForeignKey(message => message.Dialog_id);


            builder.Entity<Dialog>()
                .HasMany(dialog => dialog.Users)
                .WithMany(user => user.Dialogs)
                .Map(ud =>
                {
                    ud.ToTable("UserDialog");
                    ud.MapLeftKey("Dialog_Id");
                    ud.MapRightKey("User_Id"); 
                });

            builder.Entity<Dialog>()
                .Property(dialog => dialog.StartedAt).IsRequired();

            builder.Entity<Dialog>()
                .Property(dialog => dialog.ClosedAt).IsRequired();

/*----------------------------------- Message Table ----------------------------------------*/
            builder.Entity<Message>().ToTable("Message");
            builder.Entity<Message>()
                .HasKey<long>(message => message.Id)
                .HasRequired(message => message.Sender)
                    .WithMany(user => user.Messages)
                    .HasForeignKey(message => message.Sender_id);

            builder.Entity<Message>()
                .Property(message => message.SendedAt).IsRequired();

            builder.Entity<Message>()
               .Property(message => message.Text).IsRequired();

        }

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry,
                                                                   IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as AppUser;
                //check for uniqueness of email
                if (user != null)
                {
                    if (RequireUniqueEmail && Users.Any(u => String.Equals(u.Email, user.Email)))
                    {
                        errors.Add(new DbValidationError("User",
                            String.Format(CultureInfo.CurrentCulture, user.Email)));
                    }
                }
                if (errors.Any())
                {
                    return new DbEntityValidationResult(entityEntry, errors);
                }
            }
            return base.ValidateEntity(entityEntry, items);
        }
    }
}
