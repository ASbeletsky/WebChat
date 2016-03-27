namespace WebChat.Business.Core.Identity
{
    #region Using

    using WebChat.Domain.Core.Identity;

    #endregion
    public class Client : User
    {
    #region Constructors

        public Client(long userId) : base(userId)
        {

        }

        public Client(string name, string email) : base(name, email)
        {

        }

        public Client(UserModel model) : base(model)
        {

        }

    #endregion

    }
}
