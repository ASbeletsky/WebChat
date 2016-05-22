namespace WebChat.Data.Storage
{
    #region Using

    using Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Application;
    using Models.Chat;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Common;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Validation;
    using System.Globalization;
    using System.Linq;

    #endregion

    public class WebChatDbContext : IdentityDbContext<UserModel, UserRoleModel, long, UserLoginModel, UsersInRolesModel,  UserClaimModel>
    {
        #region Private Members

        private DbModelBuilder _modelBuilder;
        private DbModelBuilder ModelBuilder
        {
            get
            {
                if (_modelBuilder != null)
                    return _modelBuilder;
                else
                    throw new ArgumentNullException(paramName: "modelBuilder");
            }
            set
            {
                _modelBuilder = value;
            }
        }

        #endregion

        #region Constructors

        private WebChatDbContext() : base("WebChatDbContext")
        {
        }
        private WebChatDbContext(DbConnection dbConnection) : base(dbConnection, contextOwnsConnection: true)
        {
        }
        private WebChatDbContext(string connectionString) : base(connectionString)
        {
        }

        #endregion

        #region Static Members

        public static WebChatDbContext GetInstance()
        {
            return new WebChatDbContext();
        }

        public static WebChatDbContext GetInstance(string connectionString)
        {
            return new WebChatDbContext(connectionString);
        }

        public static WebChatDbContext GetInstance(DbConnection dbConnection)
        {
            return new WebChatDbContext(dbConnection);
        }

        #endregion

        #region Tables

        public virtual DbSet<CustomerApplicationModel> CustomerApplications { get; set; }
        public virtual DbSet<UsersInAppsModel> UsersInApplications { get; set; }
        public virtual DbSet<UserRoleModel> UserRoles { get; set; }
        public virtual DbSet<UsersInRolesModel> UsersInRoles { get; set; }
        public virtual DbSet<DialogModel> Dialogs { get; set; }
        public virtual DbSet<MessageModel> Messages { get; set; }

                    #endregion

        #region Table Mappings

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            base.OnModelCreating(builder);
            ModelBuilder = builder;

            this.MapUserTable();
            this.MapAppRoleTable();
            this.MapUserRoleTable();
            this.MapUserLoginTable();
            this.MapUserClaimTable();
            this.MapCustomerAppTable();
            this.MapDialogTable();
            this.MapMessageTable();
        }

        private void MapUserTable()
        {
            var userTable = ModelBuilder.Entity<UserModel>().ToTable("User");

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
        }

        private void MapAppRoleTable()
        {
            ModelBuilder.Entity<UserRoleModel>().ToTable("Role");
        }

        private void MapUserRoleTable()
        {
            ModelBuilder.Entity<UsersInRolesModel>().ToTable("UserRole");
        }

        private void MapUserLoginTable()
        {
            ModelBuilder.Entity<UserLoginModel>().ToTable("UserLogin");
        }

        private void MapUserClaimTable()
        {
            ModelBuilder.Entity<UserClaimModel>().ToTable("UserClaim");
        }
        
        private void MapCustomerAppTable()
        {
            ModelBuilder.Entity<CustomerApplicationModel>().ToTable("CustomerApplication")
                .HasKey<int>(app => app.Id)
                .HasMany(app => app.RelatedUsers)
                     .WithMany(user => user.RelatedApplications)
                     .Map(user_app =>
                     {
                         user_app.ToTable("UserApp");
                         user_app.MapLeftKey("AppId");
                         user_app.MapRightKey("UserId");
                     });

            ModelBuilder.Entity<CustomerApplicationModel>()
                .HasRequired(app => app.Owner)
                    .WithMany(user => user.myOwnApplications)
                    .HasForeignKey(app => app.OwnerId);

            ModelBuilder.Entity<CustomerApplicationModel>()
                .Property(app => app.ContactEmail).IsRequired();

            ModelBuilder.Entity<CustomerApplicationModel>()
                .Property(app => app.WebsiteUrl).IsRequired();

        }

        private void MapDialogTable()
        {
            ModelBuilder.Entity<DialogModel>().ToTable("Dialog");
            ModelBuilder.Entity<DialogModel>()
                .HasKey<int>(dialog => dialog.Id)
                .HasMany(dialog => dialog.Messages)
                    .WithRequired(message => message.Dialog)
                    .HasForeignKey(message => message.DialogId);


            ModelBuilder.Entity<DialogModel>()
                .HasMany(dialog => dialog.Users)
                .WithMany(user => user.Dialogs)
                .Map(ud =>
                {
                    ud.ToTable("UserDialog");
                    ud.MapLeftKey("Dialog_Id");
                    ud.MapRightKey("User_Id");
                });

            ModelBuilder.Entity<DialogModel>()
                .Property(dialog => dialog.StartedAt).IsRequired();

            ModelBuilder.Entity<DialogModel>()
                .Property(dialog => dialog.ClosedAt).IsRequired();
        }

        private void MapMessageTable()
        {
            ModelBuilder.Entity<MessageModel>().ToTable("Message");
            ModelBuilder.Entity<MessageModel>()
                .HasKey<long>(message => message.Id)
                .HasRequired(message => message.Sender)
                    .WithMany(user => user.Messages)
                    .HasForeignKey(message => message.SenderId);

            ModelBuilder.Entity<MessageModel>()
                .Property(message => message.SendedAt).IsRequired();

            ModelBuilder.Entity<MessageModel>()
               .Property(message => message.Text).IsRequired();
        }

        #endregion

        #region Protected Members

        protected override DbEntityValidationResult ValidateEntity(DbEntityEntry entityEntry, IDictionary<object, object> items)
        {
            if (entityEntry != null && entityEntry.State == EntityState.Added)
            {
                var errors = new List<DbValidationError>();
                var user = entityEntry.Entity as UserModel;
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

        #endregion
    }
}
