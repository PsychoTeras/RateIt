using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using RateIt.Common.Core.Entities.Users;

namespace RateIt.Common.Core.DAL
{
    internal sealed class UserDAL : BaseDAL<User>
    {

#region Constants

        internal const string IDX_T_USERS_USERNAME = "IDX_T_USERS_USERNAME";
        internal const string IDX_T_USERS_CREDENTIAL = "IDX_T_USERS_CREDENTIAL";

#endregion

#region Properties

        internal override string CollectionName
        {
            get { return "T_USERS"; }
        }

        internal MongoCollection<UserListItem> UserListDataCollection { get; private set; }

#endregion

#region Class methods

        public UserDAL()
        {
            UserListDataCollection = Database.GetCollection<UserListItem>(CollectionName);
        }

        protected override void CreateCollectionStructure()
        {
            //Create T_USER indexes, IDX_T_USERS_USERNAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("UserName");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_USERS_USERNAME).
                SetUnique(true);
            DataCollection.EnsureIndex(indexKeys, indexOptions);

            //IDX_T_USERS_CREDENTIAL
            indexKeys = IndexKeys.
                Ascending("UserName").
                Ascending("Password");
            indexOptions = IndexOptions.
                SetName(IDX_T_USERS_CREDENTIAL);
            DataCollection.EnsureIndex(indexKeys, indexOptions);
            
            //!!! What is it?
            //Server.IndexCache.Add()
        }

        public ObjectId GetUserId(string userName, string password)
        {
            string qsName = string.Format("/^({0})$/(si)", userName);
            IMongoQuery query = new QueryBuilder<User>().And(new[]
                        {
                            Query.Matches("UserName", qsName),
                            Query.EQ("Password", password)
                        });
            User user = DataCollection.FindOne(query);
            return user != null ? user.Id : ObjectId.Empty;
        }

        public User GetUser(string userName)
        {
            //Search by user name (^...$), SingleLine+CI (see Regex standart)
            string queryString = string.Format("/^({0})$/(si)", userName);
            IMongoQuery qUserByName = Query.Matches("UserName", queryString);

            //Search user
            return DataCollection.FindOne(qUserByName);
        }

        public bool IsUserExists(string userName)
        {
            return GetUser(userName) != null;
        }

        public void UserRegister(User registrationInfo)
        {
            //Register new user
            WriteConcernResult concernResult = DataCollection.Insert(registrationInfo);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

        public UserListItem[] GetUserList(UserLoginDAL userLoginDAL, 
            string userNamePart, int maxCount)
        {
            //Search by part of user name
            string queryString = string.Format("/({0})/(si)", userNamePart);
            IMongoQuery qUserByName = Query.Matches("UserName", queryString);

            //Do search
            MongoCursor<UserListItem> cursor = UserListDataCollection.
                Find(qUserByName).
                SetHint(IDX_T_USERS_USERNAME);

            //Set TOP (N) limit
            if (maxCount > 0)
            {
                cursor = cursor.SetLimit(maxCount);
            }

            //Get result list
            UserListItem[] result = cursor.ToArray();

            //Set user logged state is needed
            if (userLoginDAL != null)
            {
                foreach (UserListItem userListItem in result)
                {
                    userListItem.IsUserLogged = userLoginDAL.IsUserLogged(userListItem.Id);
                }
            }

            //Return list of users
            return result;
        }

#endregion

    }
}
