using System;

namespace RateIt.Common.Core.QueryParams
{
    [Serializable]
    public sealed class QuerySysRequestID
    {

#region Private static fields

        private static readonly QuerySysRequestID _instance = new QuerySysRequestID();

#endregion

#region Public static properties

        public static QuerySysRequestID Instance
        {
            get { return _instance; }
        }

#endregion

#region Public fields

        public int Part1;
        public int Part2;
        public int Part3;

#endregion

#region Class methods

        public QuerySysRequestID()
        {
            Part1 = 8236125;
            Part2 = 0000221;
            Part3 = -409610;
        }

        private bool Equals(QuerySysRequestID other)
        {
            return Part1 == other.Part1 &&
                   Part2 == other.Part2 &&
                   Part3 == other.Part3;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }
            if (ReferenceEquals(this, obj))
            {
                return true;
            }
            return obj is QuerySysRequestID && Equals((QuerySysRequestID)obj);
        }

        public static bool operator ==(QuerySysRequestID sysId1, QuerySysRequestID sysId2)
        {
            return !ReferenceEquals(sysId1, null) && !ReferenceEquals(sysId2, null) && sysId1.Equals(sysId2);
        }

        public static bool operator !=(QuerySysRequestID sysId1, QuerySysRequestID sysId2)
        {
            return !(sysId1 == sysId2);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Part1;
                hashCode = (hashCode*397) ^ Part2;
                hashCode = (hashCode*397) ^ Part3;
                return hashCode;
            }
        }

#endregion

    }
}
