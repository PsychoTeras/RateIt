using System;
using System.Runtime.Serialization;
using System.Text;

namespace RateIt.Common.Core.QueryResults
{
    [Serializable, DataContract]
    public class BaseQueryResult
    {

#region Properties
            
        public bool HasError
        {
            get { return !string.IsNullOrEmpty(ErrorMessage); }
        }

#endregion

#region Public fields

        [DataMember]
        public int ErrorCode;
        [DataMember]
        public string ErrorMessage;

#endregion 
        
#region Static methods

        public static BaseQueryResult Successful
        {
            get { return new BaseQueryResult(); }
        }

#endregion

#region Class methods

        public BaseQueryResult() {}

        public BaseQueryResult(string errorMessage) : this(errorMessage, 0) { }

        public BaseQueryResult(string errorMessage, int errorCode)
        {
            Set(errorMessage, errorCode);
        }

        public BaseQueryResult(Exception ex) : this(ex, 0) { }

        public BaseQueryResult(Exception ex, int errorCode)
        {
            Set(ex, errorCode);
        }

        public BaseQueryResult Set(string errorMessage, int errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
            return this;
        }

        public BaseQueryResult Set(Exception ex, int errorCode)
        {
            ErrorMessage = ex.Message;
            ErrorCode = errorCode;
            return this;
        }

        public override string ToString()
        {
            if (HasError)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Error message:\t{0}\n", ErrorMessage);
                sb.AppendFormat("Error code:\t{0}", ErrorCode);
                return sb.ToString();
            }
            return "No errors";
        }

#endregion

#region Class static methods

        public static Exception Throw(string message, int errorCode)
        {
            Exception ex = new Exception(message);
            ex.Data.Add("ErrorCode", errorCode);
            throw ex;
        }

        public static T FromException<T>(Exception ex) 
            where T : BaseQueryResult, new()
        {
            int errorCode = ex.Data.Contains("ErrorCode")
                                ? (int) ex.Data["ErrorCode"]
                                : 0;
            return FromException<T>(ex, errorCode);
        }

        public static T FromException<T>(Exception ex, int errorCode)
            where T : BaseQueryResult, new()
        {
            return (T)new T().Set(ex, errorCode);
        }

#endregion

    }
}
