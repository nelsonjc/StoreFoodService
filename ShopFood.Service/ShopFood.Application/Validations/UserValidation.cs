using ShopFood.Domain.DTOs.Requests;
using Throw;

namespace ShopFood.Application.Validations
{
    /// <summary>
    /// Class to validate User
    /// </summary>
    public static class UserValidation
    {
        /// <summary>
        /// Method to User Create Validate
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="Exception"></exception>
        public static void UserCreateValidate(UserRequest request)
        {
            request.Throw().IfNull(x => x);
            request.Name.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Username.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Password?.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.RoleId.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                    .IfEquals(default(Guid));
        }

        /// <summary>
        /// Method to User Customer Create Validate
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="Exception"></exception>
        public static void UserCustomerCreateValidate(UserRequest request)
        {
            request.Throw().IfNull(x => x);
            request.Name.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Username.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Password?.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
        }

        /// <summary>
        /// Method to User Update Validate
        /// </summary>
        /// <param name="request"></param>
        /// <exception cref="Exception"></exception>
        public static void UserUpdateValidate(UserRequest request)
        {
            request.Throw().IfNull(x => x);
            request.Id?.ThrowIfNull(paramName => throw new Exception($"Param name: {paramName}. String should not be null."));
            request.Id?.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEquals(default(Guid));
            request.Active?.ThrowIfNull(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."));
            request.Name.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Username.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.RoleId.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
            .IfEquals(default(Guid));
            request.Active?.ThrowIfNull(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."));

        }
    }
}
