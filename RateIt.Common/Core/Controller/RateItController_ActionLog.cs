using MongoDB.Bson;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.ActionLogs;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController
    {

#region Constants



#endregion

#region Private methods

        private void AddActionLogRecord(ActionLogType actionType,
            ObjectId? subject = null, string subjectInfo = null,
            ObjectId? @object = null, string objectInfo = null,
            string additionalInfo = null)
        {
            ActionLog actionLog = new ActionLog(actionType, subject,
                subjectInfo, @object, objectInfo, additionalInfo);
            _actionLogDAL.ActionLogAdd(actionLog);
        }

        private void AddActionLogRecord(ActionLogType actionType,
            byte[] subject = null, string subjectInfo = null,
            byte[] @object = null, string objectInfo = null,
            string additionalInfo = null)
        {
            //Convert byte arrays to ObjectId
            ObjectId? objSubject = subject == null
                ? (ObjectId?) null
                : subject.ToObjectId();
            ObjectId? objObject = @object == null
                ? (ObjectId?)null
                : @object.ToObjectId();

            //Create and push log record into DB
            ActionLog actionLog = new ActionLog(actionType, objSubject,
                subjectInfo, objObject, objectInfo, additionalInfo);
            _actionLogDAL.ActionLogAdd(actionLog);

            //Release ObjectId
            if (objSubject.HasValue)
            {
                objSubject.Value.Release();
            }
            if (objObject.HasValue)
            {
                objObject.Value.Release();
            }
        }

        private void AddActionLogRecord(ActionLogType actionType,
            string subject = null, string subjectInfo = null,
            string @object = null, string objectInfo = null,
            string additionalInfo = null)
        {
            //Convert strings to ObjectId
            ObjectId? objSubject = subject == null
                ? (ObjectId?)null
                : subject.ToObjectId();
            ObjectId? objObject = @object == null
                ? (ObjectId?)null
                : @object.ToObjectId();

            //Create and push log record into DB
            ActionLog actionLog = new ActionLog(actionType, objSubject,
                subjectInfo, objObject, objectInfo, additionalInfo);
            _actionLogDAL.ActionLogAdd(actionLog);

            //Release ObjectId
            if (objSubject.HasValue)
            {
                objSubject.Value.Release();
            }
            if (objObject.HasValue)
            {
                objObject.Value.Release();
            }
        }

#endregion

#region Public methods

        

#endregion

    }
}
