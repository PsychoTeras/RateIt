using System;
using MongoDB.Bson;
using RateIt.Common.Core.Constants;

namespace RateIt.Common.Core.Entities.ActionLogs
{
    [Serializable]
    internal sealed class ActionLog : BaseDocument
    {

#region Public fields

        public DateTime ActionTime;
        public ActionLogType ActionType;

        public ObjectId? Subject;
        public string SubjectInfo;

        public ObjectId? Object;
        public string ObjectInfo;

        public string AdditionalInfo;

#endregion

#region Ctor

        public ActionLog()
        {
            ActionTime = DateTime.Now.ToUniversalTime();
        }

        public ActionLog(ActionLogType actionType,
                         ObjectId? subject = null, string subjectInfo = null,
                         ObjectId? @object = null, string objectInfo = null,
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