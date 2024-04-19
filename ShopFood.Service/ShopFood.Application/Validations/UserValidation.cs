using ShopFood.Domain.DTOs.Requests;
using Throw;

namespace ShopFood.Application.Validations
{
    public static class UserValidation
    {
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
