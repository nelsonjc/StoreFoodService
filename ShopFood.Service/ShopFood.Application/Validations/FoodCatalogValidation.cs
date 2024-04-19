using ShopFood.Domain.DTOs.Requests;
using Throw;

namespace ShopFood.Application.Validations
{
    public class FoodCatalogValidation
    {
        public static void FoodCatalogCreateValidate(FoodCatalogRequest request)
        {
            request.Throw().IfNull(x => x);
            request.Name.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Code.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Description?.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Stock.Throw().IfNegative();
            request.Price.Throw().IfNegative();
            request.UserCreatedId.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEquals(default(Guid));
        }

        public static void FoodCatalogUpdateValidate(FoodCatalogRequest request)
        {
            request.Throw().IfNull(x => x);
            request.Id?.ThrowIfNull(paramName => throw new Exception($"Param name: {paramName}. String should not be null."));
            request.Id?.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEquals(default(Guid));
            request.Name.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Code.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Description.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEmpty()
                .IfWhiteSpace();
            request.Active?.ThrowIfNull(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."));
            request.Price.Throw().IfNegative();
            request.Stock.Throw().IfNegative();
            request.UserCreatedId.Throw(paramName => throw new Exception($"Param name: {paramName}. String should not be empty or white space only."))
                .IfEquals(default(Guid));
        }
    }
}
