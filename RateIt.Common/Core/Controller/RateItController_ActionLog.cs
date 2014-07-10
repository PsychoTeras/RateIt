using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.ActionLogs;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController
    {

#region Constants



#endregion

#region Private methods

        private void AddActionLogRecord(ActionLogType actionType,
            string subject = null, string subjectInfo = null,
            string @object = null, string objectInfo = null,
            string additionalInfo = null)
        {
            ActionLog actionLog = new ActionLog(actionType, @object,
                objectInfo, subject, subjectInfo, additionalInfo);
            _actionLogDAL.ActionLogAdd(actionLog);
        }

#endregion

#region Public methods

        

#endregion

    }
}
