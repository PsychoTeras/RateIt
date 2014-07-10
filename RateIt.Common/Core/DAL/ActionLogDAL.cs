using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.ActionLogs;

namespace RateIt.Common.Core.DAL
{
    internal sealed class ActionLogDAL : BaseDAL<ActionLog>
    { 

#region Constants

        internal const string IDX_T_ACTION_LOGS_ACTION_TYPE =
            "IDX_T_ACTION_LOGS_ACTION_TYPE";

        internal const string IDX_T_ACTION_LOGS_ACTION_TYPE_OBJECT_SUBJECT = 
            "IDX_T_ACTION_LOGS_ACTION_TYPE_OBJECT_SUBJECT";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_ACTION_LOGS"; }
        }

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_ACTION_LOGS indexes, IDX_T_ACTION_LOGS_ACTION_TYPE_OBJECT_SUBJECT
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("ActionType").
                Ascending("Object").
                Ascending("Subject");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_ACTION_LOGS_ACTION_TYPE_OBJECT_SUBJECT);
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_ACTION_LOGS_ACTION_TYPE
            indexKeys = IndexKeys.
                Ascending("ActionType");
            indexOptions = IndexOptions.
                SetName(IDX_T_ACTION_LOGS_ACTION_TYPE);
            DataCollection.CreateIndex(indexKeys, indexOptions);
        }

        public void ActionLogAdd(ActionLog actionLog)
        {
            //Add action log
            WriteConcernResult concernResult = DataCollection.Insert(actionLog);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

#endregion

    }
}
