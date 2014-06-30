using System.Windows.Forms;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Management.Helpers
{
    public static class Helper
    {

#region Class static methods

        public static bool CheckOnValidQueryResult(BaseQueryResult baseQueryResult)
        {
            if (baseQueryResult != null && baseQueryResult.HasError)
            {
                MessageBox.Show(baseQueryResult.ToString(), "Query error", 
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            return true;
        }

#endregion

    }
}
