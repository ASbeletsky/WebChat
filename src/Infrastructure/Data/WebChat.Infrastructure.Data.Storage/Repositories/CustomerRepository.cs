namespace WebChat.Infrastructure.Data.Storage.Repositories
{
    #region Using

    using WebChat.Infrastructure.Data.Interfaces.Repositories;
    using System;
    using System.Collections.Generic;
    using Domain.Core.Customer;
    using Services.Interfaces;
    using Models.Identity;
    using Factories;
    using Domain.Core.Identity;
    using System.Data.Entity;

    #endregion

    public class CustomerRepository : ICustomerRepository
    {
        #region Private Memebers

        private readonly WebChatDbContext context;
        private readonly IEntityConverter converter;
        private readonly UserFactory factory;

        #endregion

        #region Constructors
        public CustomerRepository(WebChatDbContext context, IEntityConverter converter, UserFactory factory)
        {
            this.context = context;
            this.converter = converter;
            this.factory = factory;
        }

        #endregion

        public IEnumerable<Customer> All
        {
            get
            {
                IEnumerable<UserModel> models = context.GetUsersInRole((int)Roles.Customer);
                IEnumerable<Customer> customers = factory.RestoreUsersFromModels(models) as IEnumerable<Customer>;
                return customers;
            }
        }

        public void Create(Customer item)
        {
            throw new NotImplementedException();
        }

        public void Delete(long id)
        {
            var recordForDelete = context.Users.Find(id);
            if (recordForDelete != null)
            {
                context.Users.Remove(recordForDelete);
                context.SaveChanges();
            }
        }

        public Customer GetById(long id)
        {            
            if(!IsCusomer(id))
            {
                throw new Exception(string.Format("User with id {0} is not a customer", id));
            }

            UserModel model = context.Users.Find(id);
            Customer customer = factory.RestoreUserFromModel(model) as Customer;
            return customer;
        }

        public void Update(Customer item)
        {
            if (!IsCusomer(item.Id))
            {
                throw new Exception(string.Format("User with id {0} is not a customer", item.Id));
            }

            UserModel model = converter.Convert<User, UserModel>(item);
            context.Entry(model).State = EntityState.Modified;
            context.SaveChanges();
        }

        private bool IsCusomer(long userId)
        {
            return context.IsUserInRole(userId, (long)Roles.Customer);
        }
    }
}
