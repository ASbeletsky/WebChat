namespace WebChat.Business.Core.Identity
{
    #region Using

    using Domain.Data;
    using Domain.Data.Managers;
    using Microsoft.AspNet.Identity;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Claims;
    using WebChat.Domain.Core.Application;
    using BusinessLogic.DomainModels;
    using Domain.Core.Identity;
    using Customer;
    using Domain.Interfaces.Repositories;
    using Services.Interfaces;
    using Ninject;
    using Domain.Interfaces;
    #endregion

    public abstract class User : BaseDomainModel
    {

    #region Private Members

        private UserModel UserModel { get; set; }

        [Inject]
        private AppUserManager UserManager { get; set; }

        [Inject]
        private IUnitOfWork UnitOfWork { get; set; }

        private Lazy<IEnumerable<CustomerApplication>> relatedApplications;

        #endregion

        #region Constructors

        public User(long userId) 
        {
            this.UserModel = UserManager.FindById(userId);
        }

        public User (string name, string email)
        {
            this.UserModel = new UserModel()
            {
                Name = name,
                Email = email,
                RegistrationDate = DateTime.Today
            };
            
        }

        public User(UserModel model)
        {
            this.UserModel = model;
        }

        #endregion

        #region Public Properties

        public long Id 
        {
            get
            {
                return UserModel.Id;
            }
        }

        public string Name 
        {
            get
            {
                return UserModel.Name;
            }
            protected set
            {
                if (!string.IsNullOrEmpty(value))
                    UserModel.Name = value;
                else
                    throw new ArgumentException("Имя не должно быть пустым", paramName: "Name");
            }
        }

        public string UserName 
        {
            get
            {
                return UserModel.UserName;
            }
            protected set
            {
                if (!string.IsNullOrEmpty(value))
                    UserModel.UserName = value;
                else
                    throw new ArgumentException("Логин не должнен быть пустым", paramName: "UserName");
            }
        }

        public string Email 
        {
            get
            {
                return UserModel.Email;
            }
            protected set
            {
                if (!string.IsNullOrEmpty(value))
                    UserModel.Email = value;
                else
                    throw new ArgumentException("Адресс эл. почты не должнен быть пустым", paramName: "Email");
            }
        }

        public DateTime RegistrationDate 
        {
            get
            {
                return UserModel.RegistrationDate;
            }
        }

        
        public IEnumerable<CustomerApplication> GetRelatedApplications()
        {
            var apps = UserModel.RelatedApplications;
            if (apps != null && apps.Any())
                return Converter.Convert<IEnumerable<CustomerApplicationModel>, IEnumerable<CustomerApplication>>(apps);
            else
                return Enumerable.Empty<CustomerApplication>();
        }

        public string PhotoSource 
        {
            get
            {
                var photoSource = UserModel.Claims.FirstOrDefault(c => c.ClaimType == AppClaimTypes.PhotoSource).ClaimValue;
                if (string.IsNullOrEmpty(photoSource))
                    return "../../../Content/Images/default-user-image.png";
                else
                    return photoSource;
            }
            private  set
            {
                var photoClaim = new Claim(type: AppClaimTypes.PhotoSource, value: value);
                var saveClaimTask = UserManager.SaveClaimAsync(this.Id, photoClaim);
                saveClaimTask.Start();
                if (saveClaimTask.IsCompleted)
                {
                    var saveResult = saveClaimTask.Result;
                    if (!saveResult.Succeeded)
                        throw new Exception(saveResult.Errors.First());
                }
            }
        }

    #endregion

    }
}
