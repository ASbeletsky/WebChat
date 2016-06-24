namespace WebChat.Business.DomainModels
{
    #region

    using System;
    using System.Collections.Generic;
    using WebChat.WebUI.ViewModels.Statistic;

    #endregion
    public class ApplicationStatisticDomainModel : BaseDomainModel
    {
        public IEnumerable<DialogPerDayChartIDataItem> GetDialogsPerDay(AppStatisticTimePeriodViewModel model)
        {
            return Storage.Applications.GetChatsCountInPeriod(model.AppId, model.StartDate, model.EndDate);
        }

        public IEnumerable<DialogsDurationPerDayChartDataItem> GetDialogsDurationPerDay(AppStatisticTimePeriodViewModel model)
        {
            return Storage.Applications.GetChatDurationInPeriod(model.AppId, model.StartDate, model.EndDate);
        }

        public MessageCountDifferenceChartDataItem GetMessageCountDifference(int appId)
        {
            return Storage.Applications.MessageCountInCurrentAndPreviosMonth(appId);
        }
    }
}
