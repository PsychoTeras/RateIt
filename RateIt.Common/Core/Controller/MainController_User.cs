using System;
using MongoDB.Bson;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class MainController
    {

#region Private methods

        private void AssertRegistrationInfo(User registrationInfo)
        {
            //Check on null-reference
            if (registrationInfo == null)
            {
                throw BaseQueryResult.Throw("Registration info is null-reference", 
                    ECGeneral.NullReference);
            }

            //Validate user name
            registrationInfo.UserName = (registrationInfo.UserName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(registrationInfo.UserName))
            {
                throw BaseQueryResult.Throw("User name cannot be blank", 
                    ECUserRegistration.UserNameIsBlank);
            }

            //Validate user password
            if (string.IsNullOrEmpty(registrationInfo.Password))
            {
                throw BaseQueryResult.Throw("Password cannot be blank", 
                    ECUserRegistration.PasswordIsBlank);
            }

            //Validate email
            registrationInfo.Email = (registrationInfo.Email ?? string.Empty).Trim();
            if (!string.IsNullOrEmpty(registrationInfo.Email) &&
                !Helper.IsValidEmail(registrationInfo.Email))
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
                    ECGeneral.NullReference);
            }

            //Validate user name
            loginInfo.UserName = (loginInfo.UserName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(loginInfo.UserName))
            {
                throw BaseQueryResult.Throw("User name cannot be blank",
                    ECLogin.UserNameIsBlank);
            }

            //Validate user password
            if (string.IsNullOrEmpty(loginInfo.Password))
            {
                throw BaseQueryResult.Throw("Password cannot be blank",
                    ECLogin.PasswordIsBlank);
            }

            //Validate crenedtials
            ObjectId userId = _userDAL.GetUserId(loginInfo.UserName, 
                                                              loginInfo.Password);
            if (userId.IsEmpty())
            {
                throw BaseQueryResult.Throw("Invalid user name or/and password",
                    ECLogin.InvalidCrenedtials);
            }

            //Check if the user logged for now
            if (_userLoginDAL.IsUserLogged(userId))
            {
                throw BaseQueryResult.Throw("This user has logged for now",
                    ECLogin.UserIsLogged);
            }

            return userId;
        }

        private void AssertLogoutInfo(ObjectId userId)
        {
            //Validate user ID
            if (userId == ObjectId.Empty)
            {
                throw BaseQueryResult.Throw("User ID is invalid or empty",
                    ECLogout.UserIdIsInvalid);
            }

            //Check if the user logged for now
            if (!_userLoginDAL.IsUserLogged(userId))
            {
                throw BaseQueryResult.Throw("This user doesn't logged for now",
                    ECLogout.UserDoesNotLogged);
            }
        }

#endregion

#region Public methods

        public BaseQueryResult UserRegister(User registrationInfo)
        {
            try
            {
                //Assert registration information
                AssertRegistrationInfo(registrationInfo);

                //Register user
                try
                {
                    _userDAL.UserRegister(registrationInfo);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<BaseQueryResult>(dbEx, ECGeneral.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<BaseQueryResult>(ex);
            }

            //Done
            return BaseQueryResult.Successful;
        }

        public UserQueryResult UserLogin(UserLoginInfo loginInfo)
        {
            try
            {
                //Assert login information
                ObjectId userId = AssertLoginInfo(loginInfo);

                //Login the user
                try
                {
                    _userLoginDAL.UserLogin(userId);
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<UserQueryResult>(dbEx, ECGeneral.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<UserQueryResult>(ex);
            }

            //Done
            return UserQueryResult.Successful;
        }

        public BaseQueryResult UserLogout(ObjectId userId)
        {
            try
            {
                //Assert logout information
                AssertLogoutInfo(userId);

                //Logout the user
                try
                {
                    _userLoginDAL.UserLogout(userId);
                }
                catch (Exception dbEx)
                {
                    return BaseQueryResult.FromException<UserQueryResult>(dbEx, ECGeneral.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<UserQueryResult>(ex);
            }

            //Done
            return UserQueryResult.Successful;
        }

        public UserListQueryResult GetUserList(string userNamePart, int maxCount)
        {
            //Normalize input parameters
            userNamePart = userNamePart ?? string.Empty;
            maxCount = maxCount < 0 ? 0 : maxCount;

            //Get list of users
            try
            {
                UserListItem[] userList = _userDAL.GetUserList(_userLoginDAL, userNamePart, maxCount);
                return new UserListQueryResult(userList);
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<UserListQueryResult>(ex, ECGeneral.DBError);
            }
        }

#endregion

    }
}
