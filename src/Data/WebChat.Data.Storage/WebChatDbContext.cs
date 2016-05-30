namespace WebChat.Data.Storage
{
    #region Using

    using Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Models.Application;
    using Models.Chat;
    using Ninject;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Validation;
    using System.Globalization;
    using System.Linq;

    #endregion

    public class WebChatDbContext : IdentityDbContext<UserModel, UserRoleModel, long, UserLoginModel, UsersInRolesModel, UserClaimModel>
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

        [Inject]
        public WebChatDbContext() : base("WebChatDbContext")
        {
        }

        private WebChatDbContext(string connectionString) : base(connectionString)
        {
        }

        #endregion

        #region Static Members

        [Inject]
        public static WebChatDbContext GetInstance()
        {
            return new WebChatDbContext();
        }

        public static WebChatDbContext GetInstance(string connectionString)
        {
            return new WebChatDbContext(connectionString);
        }

        #endregion

        #region Tables

        public virtual DbSet<ApplicationModel> CustomerApplications { get; set; }
        public virtual DbSet<UsersInAppsModel> UsersInApplications { get; set; }
        public virtual DbSet<UsersInRolesModel> UsersInRoles { get; set; }
        public virtual DbSet<UsersInDialogsModel> UsersInDialogs { get; set; }
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
            this.MapUsersInAppTable();
            this.MapUsersInDialogs();
            this.MapDialogTable();
            this.MapMessageTable();
        }

        private void MapUserTable()
        {
            var userTable = ModelBuilder.Entity<UserModel>().ToTable("User");

            userTable.Property(user => user.Email)
                     .IsOptional()
                     .HasColumnAnnotation("Index", new IndexAnnotation(new IndexAttribute("EmailUniqueIndex") { IsUnique = true }));

            userTable.Property(user => user.RegistrationDate).IsOptional();

            userTable.HasMany(user => user.ApplicationsShortInfo)
                     .WithRequired(userApp => userApp.User)
                     .HasForeignKey(userApp => userApp.UserId)
                     .WillCascadeOnDelete(true);

            userTable.HasMany(user => user.DialogsShortInfo)
                     .WithRequired(dialogInfo => dialogInfo.User)
                     .HasForeignKey(dialogInfo => dialogInfo.UserId)
                     .WillCascadeOnDelete(true);
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

        private void MapUsersInAppTable()
        {
            var usersInApps = ModelBuilder.Entity<UsersInAppsModel>().ToTable("UserApp");
            usersInApps.HasKey(table => new { table.AppId, table.UserId });
        }

        private void MapCustomerAppTable()
        {
            var customerTable = ModelBuilder.Entity<ApplicationModel>().ToTable("CustomerApplication");
            customerTable.HasKey<int>(app => app.Id);

            customerTable.HasRequired(app => app.Customer)
                         .WithMany(user => user.myOwnApplications)
                         .HasForeignKey(app => app.CustomerId);

            customerTable.Property(app => app.ContactEmail).IsRequired();

            customerTable.Property(app => app.WebsiteUrl).IsRequired();

            customerTable.HasMany(app => app.UsersShortInfo)
                         .WithRequired(userApp => userApp.App)
                         .HasForeignKey(userApp => userApp.AppId)
                         .WillCascadeOnDelete(true);
        }

        private void MapUsersInDialogs()
        {
            var usersInDialogs = ModelBuilder.Entity<UsersInDialogsModel>().ToTable("UserDialog");
            usersInDialogs.HasKey(table => new { table.DialogId, table.UserId });
        }

        private void MapDialogTable()
        {
            var dialogTable = ModelBuilder.Entity<DialogModel>().ToTable("Dialog");

            dialogTable.HasKey<int>(dialog => dialog.Id)
                       .HasMany(dialog => dialog.Messages)
                       .WithRequired(message => message.Dialog)
                       .HasForeignKey(message => message.DialogId);

            dialogTable.HasMany(dialog => dialog.UsersShortInfo)
                         .WithRequired(userInDialog => userInDialog.Dialog)
                         .HasForeignKey(userApp => userApp.DialogId)
                         .WillCascadeOnDelete(true);

            dialogTable.Property(dialog => dialog.StartedAt).IsRequired();

            dialogTable.Property(dialog => dialog.ClosedAt).IsRequired();
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
