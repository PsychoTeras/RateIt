using System;
using System.Windows.Forms;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Management.Helpers
{
    public static class Helper
    {

#region Delegates

        public delegate void OnQueryResultError(string message, int errorCode);

#endregion

#region Static events

        public static Action UserSessionExpired;
        public static OnQueryResultError QueryResultError;

#endregion

#region Class static methods

        public static bool CheckOnValidQueryResult(BaseQueryResult baseQueryResult)
        {
            if (baseQueryResult != null && baseQueryResult.HasError)
            {
                if (baseQueryResult.ErrorCode == ECGeneric.InvalidSessionInfo &&
                    UserSessionExpired != null)
                {
                    UserSessionExpired();
                }
                if (QueryResultError != null)
                {
                    QueryResultError(baseQueryResult.ErrorMessage, baseQueryResult.ErrorCode);
                }
                MessageBox.Show(baseQueryResult.ToString(), "Query error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

#endregion

    }
}
