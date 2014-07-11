using System;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.DAL
{
    internal sealed class UserSessionDAL : BaseDAL<UserLogged>
    {

#region Constants

        internal const string IDX_T_LOGGED_USERS_USER_ID = "IDX_T_LOGGED_USERS_USER_ID";
        internal const string IDX_T_LOGGED_USERS_USER_ID_SESSION_ID = "IDX_T_LOGGED_USERS_USER_ID_SESSION_ID";
        internal const string IDX_T_LOGGED_USERS_LAST_ACCESS_TIME = "IDX_T_LOGGED_USERS_LAST_ACCESS_TIME";

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
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_LOGGED_USERS_USER_ID_SESSION_ID
            indexKeys = IndexKeys.
                Ascending("UserId").
                Ascending("_id");
            indexOptions = IndexOptions.
                SetName(IDX_T_LOGGED_USERS_USER_ID_SESSION_ID).
                SetUnique(true);
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_LOGGED_USERS_LAST_ACCESS_TIME
            indexKeys = IndexKeys.
                Ascending("LastAccessTime");
            indexOptions = IndexOptions.
                SetName(IDX_T_LOGGED_USERS_LAST_ACCESS_TIME).
                SetTimeToLive(TimeSpan.FromMilliseconds(0));
            DataCollection.CreateIndex(indexKeys, indexOptions);
        }

        public bool IsUserLogged(ObjectId userId)
        {
            IMongoQuery qUserById = Query.EQ("UserId", userId);
            return DataCollection.FindOne(qUserById) != null;
        }

        public bool UpdateUserSession(UserSessionInfo sessionInfo, bool assertOnly)
        {
            //Get a logged user record by session info
            ObjectId userId = sessionInfo.UserId.ToObjectId();
            ObjectId sessionId = sessionInfo.SessionId.ToObjectId();
            IMongoQuery qUserBySessionInfo = new QueryBuilder<UserLogged>().And(new[]
                {
                    Query.EQ("UserId", userId),
                    Query.EQ("_id", sessionId)
                });
            userId.Release();
            sessionId.Release();

            UserLogged userLogged = DataCollection.FindOne(qUserBySessionInfo);

            //If a logged user record doesn`t exist, return false
            if (userLogged == null)
            {
                return false;
            }

            if (assertOnly)
            {
                return true;
            }

            //Update last access time for the user record, create an update query
            DateTime newLastAccessTime = userLogged.ResetLastAccessTime();
            UpdateBuilder<UserLogged> update = Update<UserLogged>.Set
                (
                    ul => ul.LastAccessTime, newLastAccessTime
                );

            //Update the logged user record
            WriteConcernResult concernResult = DataCollection.Update(qUserBySessionInfo, update);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);

            //Return result
            return concernResult.DocumentsAffected > 0;
        }

        public UserLogged UserLogin(ObjectId userId)
        {
            //New UserLogged object for a user
            UserLogged userLogged = new UserLogged(userId);

            //Login user
            WriteConcernResult concernResult = DataCollection.Insert(userLogged);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);            

            //Return session ID
            return userLogged;
        }

        public void UserLogout(UserSessionInfo sessionInfo)
        {
            //Logout user
            ObjectId userId = sessionInfo.UserId.ToObjectId();
            ObjectId sessionId = sessionInfo.SessionId.ToObjectId();
            IMongoQuery qRemoveUserBySessionInfo = new QueryBuilder<UserLogged>().And(new[]
                {
                    Query.EQ("UserId", userId),
                    Query.EQ("_id", sessionId)
                });
            userId.Release();
            sessionId.Release();

            WriteConcernResult concernResult = DataCollection.Remove(qRemoveUserBySessionInfo);

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
