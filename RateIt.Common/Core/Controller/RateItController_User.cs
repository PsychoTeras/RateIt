using System;
using MongoDB.Bson;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryParams;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController
    {

#region Private methods

        private void AssertUserInfo(string tId, User user)
        {
            //Assert TID
            AssertTID(tId);

            //Check on null-reference
            if (user == null)
            {
                throw BaseQueryResult.Throw("User is null-reference", 
                    ECGeneric.NullReference);
            }

            //Validate user name
            user.UserName = (user.UserName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(user.UserName))
            {
                throw BaseQueryResult.Throw("User name cannot be blank", 
                    ECUserRegistration.UserNameIsBlank);
            }

            //Validate minimal user name length
            if (user.UserName.Length < GenericConstants.USER_NAME_LENGTH_MIN)
            {
                string errMsg = string.Format("User name should have {0} letters at least",
                                              GenericConstants.USER_NAME_LENGTH_MIN);
                throw BaseQueryResult.Throw(errMsg,
                    ECUserRegistration.MinUserNameLengthRequired);
            }

            //Validate maximal user name length
            if (user.UserName.Length > GenericConstants.USER_NAME_LENGTH_MAX)
            {
                string errMsg = string.Format("User name should not have more than {0} letters",
                                              GenericConstants.USER_NAME_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECUserRegistration.MaxUserNameLengthExceeded);
            }

            //Validate user name
            if (!InternalHelper.IsValidUserName(user.UserName))
            {
                throw BaseQueryResult.Throw("User name is invalid",
                    ECUserRegistration.InvalidUserName);
            }

            //Validate user password
            if (string.IsNullOrEmpty(user.PasswordHash))
            {
                throw BaseQueryResult.Throw("Password cannot be blank", 
                    ECUserRegistration.PasswordIsBlank);
            }

            //Validate password hash length
            if (user.PasswordHash.Length > GenericConstants.USER_PASSWORD_HASH_LENGTH)
            {
                throw BaseQueryResult.Throw("Password hash is invalid",
                    ECUserRegistration.InvalidPasswordHash);
            }

            //Validate email
            user.Email = (user.Email ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(user.Email) &&
                !InternalHelper.IsValidEmail(user.Email))
            {
                throw BaseQueryResult.Throw("Email address not valid", 
                    ECUserRegistration.InvalidEmail);
            }

            //Check if user exists
            if (_userDAL.IsUserExists(user.UserName))
            {
                throw BaseQueryResult.Throw("User is exists",
                    ECUserRegistration.UserNameIsBlank);                
            }
        }

        private ObjectId AssertLoginInfo(UserLoginInfo loginInfo)
        {
            //Check on null-reference
            if (loginInfo == null)
            {
                throw BaseQueryResult.Throw("Login info is null-reference",
                    ECGeneric.NullReference);
            }

            //Assert TID
            AssertTID(loginInfo.TId);

            //Validate user name
            loginInfo.UserName = (loginInfo.UserName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(loginInfo.UserName))
            {
                throw BaseQueryResult.Throw("User name cannot be blank",
                    ECLogin.UserNameIsBlank);
            }

            //Validate minimal user name length
            if (loginInfo.UserName.Length < GenericConstants.USER_NAME_LENGTH_MIN)
            {
                string errMsg = string.Format("User name should have {0} letters at least",
                                              GenericConstants.USER_NAME_LENGTH_MIN);
                throw BaseQueryResult.Throw(errMsg,
                    ECLogin.MinUserNameLengthRequired);
            }

            //Validate maximal user name length
            if (loginInfo.UserName.Length > GenericConstants.USER_NAME_LENGTH_MAX)
            {
                string errMsg = string.Format("User name should not have more than {0} letters",
                                              GenericConstants.USER_NAME_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECLogin.MaxUserNameLengthExceeded);
            }

            //Validate user name
            if (!InternalHelper.IsValidUserName(loginInfo.UserName))
            {
                throw BaseQueryResult.Throw("User name is invalid",
                    ECLogin.InvalidUserName);
            }

            //Validate user password
            if (string.IsNullOrEmpty(loginInfo.PasswordHash))
            {
                throw BaseQueryResult.Throw("Password cannot be blank",
                    ECLogin.PasswordIsBlank);
            }

            //Validate password hash length
            if (loginInfo.PasswordHash.Length > GenericConstants.USER_PASSWORD_HASH_LENGTH)
            {
                throw BaseQueryResult.Throw("Password hash is invalid",
                    ECLogin.InvalidPasswordHash);
            }

            //Validate crenedtials
            ObjectId userId = _userDAL.GetUserId(loginInfo.UserName, loginInfo.PasswordHash);
            if (userId.IsEmpty())
            {
                throw BaseQueryResult.Throw("Invalid user name or/and password",
                    ECLogin.InvalidCrenedtials);
            }

            //Check if the user is logged for now
            if (_userSessionDAL.IsUserLogged(userId))
            {
                throw BaseQueryResult.Throw("This user is currently logged",
                    ECLogin.UserIsLogged);
            }

            return userId;
        }

#endregion

#region Public methods

        public BaseQueryResult UserRegister(string tId, User user)
        {
            try
            {
                //Assert user information
                AssertUserInfo(tId, user);

                //Register user
                try
                {
                    _userDAL.UserRegister(user);

                    //Add log record
                    AddActionLogRecord(ActionLogType.User_Register, user.Id);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;
        }

        public UserLoginQueryResult UserLogin(UserLoginInfo loginInfo)
        {
            try
            {
                //Assert login information
                ObjectId userId = AssertLoginInfo(loginInfo);

                //Login the user
                try
                {
                    UserLogged userLogged = _userSessionDAL.UserLogin(userId);
                    
                    //Add log record
                    AddActionLogRecord(ActionLogType.User_Login, userId);

                    //Return login info
                    return new UserLoginQueryResult(userId.ToByteArray(), userLogged.Id.ToByteArray());
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<UserLoginQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<UserLoginQueryResult>(ex);
            }
        }

        public BaseQueryResult UserLogout(UserSessionInfo sessionInfo)
        {
            try
            {
                //Assert session information
                AssertSessionInfo(sessionInfo, true);

                //Logout the user
                try
                {
                    _userSessionDAL.UserLogout(sessionInfo);

                    //Add log record
                    AddActionLogRecord(ActionLogType.User_Logout, sessionInfo.UserId);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;
        }

#endregion

#region System methods

        public BaseQueryResult UserLogoutSys(QuerySysRequestID sysId, string userName)
        {
            try
            {
                //Assert QuerySysRequestID
                AssertQuerySysRequestID(sysId);

                try
                {
                    //Get user by name
                    User user = _userDAL.GetUser(userName);

                    //Logout the user
                    _userSessionDAL.UserLogout(user.Id);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;

        }

        public UserListQueryResult GetUserListSys(QuerySysRequestID sysId, string userNamePart, 
                                                  uint maxCount)
        {
            //Assert QuerySysRequestID
            AssertQuerySysRequestID(sysId);

            //Normalize input parameters
            userNamePart = userNamePart ?? string.Empty;

            //Get list of users
            try
            {
                User[] userList = _userDAL.GetUserList(_userSessionDAL, userNamePart, maxCount);
                return new UserListQueryResult(userList);
            }
            catch (Exception ex)
            {
                //Something failed in DB
                return BaseQueryResult.FromException<UserListQueryResult>(ex, ECGeneric.DBError);
            }
        }

#endregion

    }
}
