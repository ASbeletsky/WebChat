namespace WebChat.Domain.Core.ValueObjects
{
    #region Using

    using System;
    using WebChat.Domain.Interfaces;

    #endregion

    public class UserAuthentificationInfo : ValueObject<UserAuthentificationInfo>
    {
        public int AccessFailedCount { get; private set; }
        public bool EmailConfirmed { get; private set; }
        public bool LockoutEnabled { get; private set; }
        public DateTime? LockoutEndDateUtc { get; private set; }
        public string PasswordHash { get; private set; }
        public bool PhoneNumberConfirmed { get; private set; }
        public string SecurityStamp { get; private set; }
        public bool TwoFactorEnabled { get; private set; }
        public string Login { get; private set; }
        public DateTime RegistrationDate { get; private set; }
    }
}
