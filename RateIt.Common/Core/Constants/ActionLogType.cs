namespace RateIt.Common.Core.Constants
{
    internal enum ActionLogType : ushort
    {
        //User
        User_Register   = 100,
        User_EditInfo   = 101,
        User_Blocked    = 102,
        User_Deleted    = 103,
        User_Login      = 104,
        User_Logout     = 105,

        //Store
        Store_Register  = 200,
        Store_EditInfo  = 201,
        Store_Deleted   = 202,
    }
}
