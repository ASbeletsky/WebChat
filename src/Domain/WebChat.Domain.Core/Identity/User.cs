namespace WebChat.Business.Core.Identity
{
    #region Using

    using System;

    #endregion

    public abstract class User 
    {

        #region Private Members

        #endregion

        #region Constructors

        #endregion

        #region Public Properties

        public long Id 
        {
            get;
        }

        public string Name 
        {
            get;            
        }

        public string UserName 
        {
            get;          
        }

        public string Email 
        {
            get;           
        }

        public DateTime RegistrationDate 
        {
            get;           
        }

        #endregion

        //public IEnumerable<CustomerApplication> GetRelatedApplications()
        //{
        //    var apps = UserModel.RelatedApplications;
        //    if (apps != null && apps.Any())
        //        return Converter.Convert<IEnumerable<CustomerApplicationModel>, IEnumerable<CustomerApplication>>(apps);
        //    else
        //        return Enumerable.Empty<CustomerApplication>();
        //}

        //public string PhotoSource 
        //{
        //    get
        //    {
        //        var photoSource = UserModel.Claims.FirstOrDefault(c => c.ClaimType == AppClaimTypes.PhotoSource).ClaimValue;
        //        if (string.IsNullOrEmpty(photoSource))
        //            return "../../../Content/Images/default-user-image.png";
        //        else
        //            return photoSource;
        //    }
        //    private  set
        //    {
        //        var photoClaim = new Claim(type: AppClaimTypes.PhotoSource, value: value);
        //        var saveClaimTask = UserManager.SaveClaimAsync(this.Id, photoClaim);
        //        saveClaimTask.Start();
        //        if (saveClaimTask.IsCompleted)
        //        {
        //            var saveResult = saveClaimTask.Result;
        //            if (!saveResult.Succeeded)
        //                throw new Exception(saveResult.Errors.First());
        //        }
        //    }
    }
}

