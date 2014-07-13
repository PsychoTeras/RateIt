using System;
using RateIt.Common.Core.Constants;
using RateIt.Common.Core.Entities.Products;
using RateIt.Common.Core.Entities.Users;
using RateIt.Common.Core.ErrorCodes;
using RateIt.Common.Core.QueryResults;

namespace RateIt.Common.Core.Controller
{
    public sealed partial class RateItController
    {

#region Private methods

        private void AssertProductInfo(Product product)
        {
            //Check user on null-reference
            if (product == null)
            {
                throw BaseQueryResult.Throw("Store is null-reference",
                    ECGeneric.NullReference);
            }

            //Validate product name
            product.ProductName = (product.ProductName ?? string.Empty).Trim();
            if (string.IsNullOrEmpty(product.ProductName))
            {
                throw BaseQueryResult.Throw("Product name cannot be blank",
                    ECProductRegistration.ProductNameIsBlank);
            }

            //Validate minimal product name length
            if (product.ProductName.Length < GenericConstants.PRODUCT_NAME_LENGTH_MIN)
            {
                string errMsg = string.Format("Product name should have {0} letters at least",
                                              GenericConstants.PRODUCT_NAME_LENGTH_MIN);
                throw BaseQueryResult.Throw(errMsg,
                    ECProductRegistration.MinProductNameLengthRequired);
            }

            //Validate maximal product name length
            if (product.ProductName.Length > GenericConstants.PRODUCT_NAME_LENGTH_MAX)
            {
                string errMsg = string.Format("Product name should not have more than {0} letters",
                                              GenericConstants.PRODUCT_NAME_LENGTH_MAX);
                throw BaseQueryResult.Throw(errMsg,
                    ECProductRegistration.MaxProductNameLengthExceeded);
            }

//
//            //Check if user exists
//            if (_userDAL.IsUserExists(user.UserName))
//            {
//                throw BaseQueryResult.Throw("User is exists",
//                    ECUserRegistration.UserNameIsBlank);
//            }
        }

#endregion

#region Public methods

        public ProductRegisterQueryResult ProductRegister(UserSessionInfo sessionInfo, 
                                                          Product product)
        {
            try
            {
                //Assert product information
                AssertProductInfo(product);

                //Register product
                try
                {
                    _productDAL.ProductRegister(product);

                    //Add log record
                    AddActionLogRecord(ActionLogType.Product_Register, product.Id);

                    //Return result
                    return new ProductRegisterQueryResult(product.Id.ToByteArray());
                }
                catch (Exception dbEx)
                {
                    //Something failed in DB
                    return BaseQueryResult.FromException<ProductRegisterQueryResult>(dbEx, ECGeneric.DBError);
                }
            }
            catch (Exception ex)
            {
                return BaseQueryResult.FromException<ProductRegisterQueryResult>(ex);
            }
        }

#endregion

#region System methods

#endregion

    }
}
