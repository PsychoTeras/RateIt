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

#endregion

#region Class methods

        protected override void CreateCollectionStructure()
        {
            //Create T_USERS indexes, IDX_T_USERS_USERNAME
            IndexKeysBuilder indexKeys = IndexKeys.
                Ascending("UserName");
            IndexOptionsBuilder indexOptions = IndexOptions.
                SetName(IDX_T_USERS_USERNAME).
                SetUnique(true);
            DataCollection.CreateIndex(indexKeys, indexOptions);

            //IDX_T_USERS_CREDENTIAL
            indexKeys = IndexKeys.
                Ascending("UserName").
                Ascending("PasswordHash");
            indexOptions = IndexOptions.
                SetName(IDX_T_USERS_CREDENTIAL);
            DataCollection.CreateIndex(indexKeys, indexOptions);
        }

        public ObjectId GetUserId(string userName, string password)
        {
            string qsName = string.Format("/^({0})$/(si)", userName);
            IMongoQuery query = new QueryBuilder<User>().And(new[]
                        {
                            Query.Matches("UserName", qsName),
                            Query.EQ("PasswordHash", password)
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

        public void UserRegister(User user)
        {
            //Register new user
            WriteConcernResult concernResult = DataCollection.Insert(user);

            //Assert possible internal DB error
            AssertErrorMessage(concernResult.ErrorMessage);
        }

        public User[] GetUserList(UserSessionDAL userLoginDAL, string userNamePart, 
                                  uint maxCount)
        {
            //Search by part of user name
            string queryString = string.Format("/({0})/(si)", userNamePart);
            IMongoQuery qUserByName = Query.Matches("UserName", queryString);

            //Do search
            MongoCursor<User> cursor = DataCollection.
                Find(qUserByName).
                SetHint(IDX_T_USERS_USERNAME);

            //Set TOP (N) limit
            if (maxCount > 0)
            {
                cursor = cursor.SetLimit((int) maxCount);
            }

            //Get result list
            User[] result = cursor.ToArray();

            //Set user logged state is needed
            if (userLoginDAL != null)
            {
                foreach (User user in result)
                {
                    user.IsUserLogged = userLoginDAL.IsUserLogged(user.Id);
                }
            }

            //Return list of users
            return result;
        }

#endregion

    }
}
