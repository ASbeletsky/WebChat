namespace WebChat.BusinessLogic.DomainModels
{
    #region Using

    using WebChat.DataAccess.Data;

    #endregion

    public class DialogDomainModel : BaseDomainModel
    {
        public DialogDomainModel(IDataService storage) : base(storage)  {   }

        public IEnumerable<AgentsDialogsDataDTO> getAgentsAmountOfDialogData(string role)
        {
            return Storage.E .Database.SqlQuery<AgentsDialogsDataDTO>(NumberOfDialogsByRole, role);
        }
    }
}
