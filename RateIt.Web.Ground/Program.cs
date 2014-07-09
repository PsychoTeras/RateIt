using RateIt.Common.Core.Entities.Session;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.QueryResults;
using RateIt.Common.Helpers;
using RateIt.Web.Ground.RateItWebService;

namespace RateIt.Web.Ground
{
    class Program
    {

        private static readonly RateItControllerClient _service = new RateItControllerClient();

        static void Main(string[] args)
        {
            string passwordHash = CommonHelper.GetHashSum("1");
            UserLoginInfo loginInfo = new UserLoginInfo("1", passwordHash);
            UserLoginQueryResult loginResult = _service.UserLogin(loginInfo);
            if (!loginResult.HasError)
            {
                SessionInfo sessionInfo = new SessionInfo(loginResult.UserName, loginResult.SessionId);
                BaseQueryResult logoutResult = _service.UserLogout(sessionInfo);
            }
        }
    }
}
