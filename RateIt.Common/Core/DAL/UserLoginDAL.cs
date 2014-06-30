using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Users;

namespace RateIt.Common.Core.DAL
{
    internal sealed class UserLoginDAL : BaseDAL<UserLogged>
    {

#region Constants

        internal const string IDX_T_LOGGED_USERS_USER_ID = "IDX_T_LOGGED_USERS_USER_ID";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_LOGGED_USERS"; }
        }

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_USER indexes, IDX_T_USERS_USERNAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("UserId");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_LOGGED_USERS_USER_ID).
                SetUnique(true);
            DataCollection.EnsureIndex(indexKeys, indexOptions);
        }

        public bool IsUserLogged(ObjectId userId)
        {
            IMongoQuery qUserById = Query.EQ("UserId", userId);
            return DataCollection.FindOne(qUserById) != null;
        }

        public void UserLogin(ObjectId userId)
        {
            //New UserLogged object for a user
            UserLogged userLogged = new UserLogged(userId);

            //Login user
            WriteConcernResult concernResult = DataCollection.Insert(userLogged);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);            
        }

        public void UserLogout(ObjectId userId)
        {
            //Logout user
            IMongoQuery qRemoveUserById = Query.EQ("UserId", userId);
            WriteConcernResult concernResult = DataCollection.Remove(qRemoveUserById);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

#endregion

    }
}
