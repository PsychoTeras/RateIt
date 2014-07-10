using System;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.ActionLogs
{
    [Serializable]
    internal sealed class ActionLog : BaseDocument
    {

#region Public fields

        public DateTime ActionTime;
        public ActionLogType ActionType;

        public string Subject;
        public string SubjectInfo;

        public string Object;
        public string ObjectInfo;

        public string AdditionalInfo;

#endregion

#region Ctor

        public ActionLog()
        {
            ActionTime = DateTime.Now.ToUniversalTime();
        }

        public ActionLog(ActionLogType actionType,
                         string subject = null, string subjectInfo = null,
                         string @object = null, string objectInfo = null,
                         string additionalInfo = null)
            : this()
        {
            ActionType = actionType;
            Subject = subject;
            SubjectInfo = subjectInfo;
            Object = @object;
            ObjectInfo = objectInfo;
            AdditionalInfo = additionalInfo;
        }

#endregion

    }
}