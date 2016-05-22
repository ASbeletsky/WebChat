namespace WebChat.Infrastructure.CQRS.Queries.Application
{
    #region Using

    using Interfaces;

    #endregion

    public class EditApplicationQuery : IQuery
    {
        public int AppId { get; set; }
    }
}
