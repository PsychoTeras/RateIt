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

        private void AssertQuerySysRequestID(QuerySysRequestID sysId)
        {
            //Check system request id
            if (sysId != QuerySysRequestID.Instance)
            {
                throw BaseQueryResult.Throw("Invalid system request id",
                    ECGeneric.InvalidSysRequestId);
            }
        }

        private void AssertRegistrationInfo(string tId, User registrationInfo)
        {
            //Check on null-reference
            if (registrationInfo == null)
            {
                throw BaseQueryResult.Throw("Registration info is null-reference", 
                    ECGeneric.NullReference);
            }

            //Assert TID
            AssertTID(tId);

            //Validate user name
            registrationInfo.UserName = (registrationInfo.UserName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(registrationInfo.UserName))
            {
                throw BaseQueryResult.Throw("User name cannot be blank", 
                    ECUserRegistration.UserNameIsBlank);
            }

            //Validate user password
            if (string.IsNullOrEmpty(registrationInfo.PasswordHash))
            {
                throw BaseQueryResult.Throw("Password cannot be blank", 
                    ECUserRegistration.PasswordIsBlank);
            }

            //Validate email
            registrationInfo.Email = (registrationInfo.Email ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(registrationInfo.Email) &&
                !InternalHelper.IsValidEmail(registrationInfo.Email))
            {
                throw BaseQueryResult.Throw("Email address not valid", 
                    ECUserRegistration.InvalidEmail);
            }

            //Check if user exists
            if (_userDAL.IsUserExists(registrationInfo.UserName))
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

            //Validate user password
            if (string.IsNullOrEmpty(loginInfo.PasswordHash))
            {
                throw BaseQueryResult.Throw("Password cannot be blank",
                    ECLogin.PasswordIsBlank);
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

        private void AssertSessionInfo(UserSessionInfo sessionInfo)
        {
            AssertSessionInfo(sessionInfo, false);
        }

        private void AssertSessionInfo(UserSessionInfo sessionInfo, bool assertOnly)
        {
            //Validate session info
            if (sessionInfo == null)
            {
                throw BaseQueryResult.Throw("Session info is null-reference. Do login before continue",
                    ECGeneric.InvalidSessionInfo);
            }

            //Check user ID
            if (sessionInfo.SessionId == null)
            {
                throw BaseQueryResult.Throw("Session ID is empty",
                    ECGeneric.InvalidSessionInfo);
            }

            //Validate session
            if (!_userSessionDAL.UpdateUserSession(sessionInfo, assertOnly))
            {
                throw BaseQueryResult.Throw("User session is expired or invalid. Please re-login",
                    ECGeneric.InvalidSessionInfo);
            }
        }

#endregion

#region Public methods

        public BaseQueryResult UserRegister(string tId, User registrationInfo)
        {
            try
            {
                //Assert registration information
                AssertRegistrationInfo(tId, registrationInfo);

                //Register user
                try
                {
                    _userDAL.UserRegister(registrationInfo);

                    //Add log record
                    AddActionLogRecord(ActionLogType.User_Register, registrationInfo.Id);
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
                    return new UserLoginQueryResult(userId.ToByteArray(), loginInfo.UserName, 
                                                    userLogged.Id.ToByteArray());
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
