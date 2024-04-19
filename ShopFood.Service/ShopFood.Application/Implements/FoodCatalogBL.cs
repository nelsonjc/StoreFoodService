using AutoMapper;
using ShopFood.Application.Validations;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Application.Implements
{
    public class FoodCatalogBL : IFoodCatalogBL
    {
        private readonly IMapper _mapper;
        private readonly IFoodCatalogRepository _foodCatalogRepository;

        public FoodCatalogBL(IMapper mapper, IFoodCatalogRepository foodCatalogRepository)
        {
            _mapper = mapper;
            _foodCatalogRepository = foodCatalogRepository;
        }

        public async Task DeleteAsync(Guid id) => await _foodCatalogRepository.DeleteAsync(id);

        public async Task<IEnumerable<FoodCatalogCustomerDto>> GetAllCustomerAsync()
        {
            var result = await _foodCatalogRepository.GetAllAsync();
            if (result != null && result.Count() > 0 && result.Any(x => x.Active))
                return _mapper.Map<IEnumerable<FoodCatalogCustomerDto>>(result.Where(x => x.Active));

            return null;
        }

        public async Task<IEnumerable<FoodCatalogDto>> GetAllAsync()
        {
            var result = await _foodCatalogRepository.GetAllAsync();
            if (result != null && result.Count() > 0)
                return _mapper.Map<IEnumerable<FoodCatalogDto>>(result);

            return null;
        }

        public async Task<FoodCatalogDto> GetByIdAsync(Guid id)
        {
            var result = await _foodCatalogRepository.GetByIdAsync(id);

            if (result != null)
                return _mapper.Map<FoodCatalogDto>(result);

            return null;
        }

        public async Task InsertAsync(FoodCatalogRequest entity)
        {
            FoodCatalogValidation.FoodCatalogCreateValidate(entity);
            var dataFoodCatalog = _mapper.Map<FoodCatalog>(entity);
            await _foodCatalogRepository.InsertAsync(dataFoodCatalog);
        }

        public async Task UpdateAsync(FoodCatalogRequest entity)
        {
            FoodCatalogValidation.FoodCatalogUpdateValidate(entity);
            var dataFoodCatalog = _mapper.Map<FoodCatalog>(entity);
            await _foodCatalogRepository.UpdateAsync(dataFoodCatalog);
        }
    }
}
