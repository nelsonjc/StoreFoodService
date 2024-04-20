using AutoMapper;
using ShopFood.Application.Validations;
using ShopFood.Domain.DTOs.Requests;
using ShopFood.Domain.DTOs.Results;
using ShopFood.Domain.Entities;
using ShopFood.Domain.Interfaces.Application.Implements;
using ShopFood.Domain.Interfaces.Repository;

namespace ShopFood.Application.Implements
{
    /// <summary>
    /// Class to Food Catalog business logic
    /// </summary>
    public class FoodCatalogBL : IFoodCatalogBL
    {
        #region Variables
        private readonly IMapper _mapper;
        private readonly IFoodCatalogRepository _foodCatalogRepository;
        #endregion

        #region Ctor
        /// <summary>
        /// Ctor to Food Catalog bussiness logic
        /// </summary>
        /// <param name="mapper">Parameter mapper type of AutoMapper</param>
        /// <param name="foodCatalogRepository">Parameter type of Repository to get and set data base</param>
        public FoodCatalogBL(IMapper mapper, IFoodCatalogRepository foodCatalogRepository)
        {
            _mapper = mapper;
            _foodCatalogRepository = foodCatalogRepository;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Method to delete a food catalog
        /// </summary>
        /// <param name="id">Parameter to identify the food catalod to delete</param>
        public async Task DeleteAsync(Guid id) => await _foodCatalogRepository.DeleteAsync(id);

        /// <summary>
        /// Method to get all food catalogs to customer with food catalog actives
        /// </summary>
        /// <returns>List of food catalog actives result</returns>
        public async Task<IEnumerable<FoodCatalogCustomerDto>> GetAllCustomerAsync()
        {
            var result = await _foodCatalogRepository.GetAllAsync();
            if (result != null && result.Count() > 0 && result.Any(x => x.Active))
                return _mapper.Map<IEnumerable<FoodCatalogCustomerDto>>(result.Where(x => x.Active));

            return null;
        }

        /// <summary>
        /// Method to get all food catalogs
        /// </summary>
        /// <returns>List of food catalog result</returns>
        public async Task<IEnumerable<FoodCatalogDto>> GetAllAsync()
        {
            var result = await _foodCatalogRepository.GetAllAsync();
            if (result != null && result.Count() > 0)
                return _mapper.Map<IEnumerable<FoodCatalogDto>>(result);

            return null;
        }

        /// <summary>
        /// Method to get a food catalog by id
        /// </summary>
        /// <param name="id">Parameter to identify the food catalog to get</param>
        /// <returns>User Data Info Result</returns>
        public async Task<FoodCatalogDto> GetByIdAsync(Guid id)
        {
            var result = await _foodCatalogRepository.GetByIdAsync(id);

            if (result != null)
                return _mapper.Map<FoodCatalogDto>(result);

            return null;
        }

        /// <summary>
        /// Method to create a food catalog
        /// </summary>
        /// <param name="entity">Parameter with food catalog information to create</param>
        public async Task InsertAsync(FoodCatalogRequest entity)
        {
            FoodCatalogValidation.FoodCatalogCreateValidate(entity);
            var dataFoodCatalog = _mapper.Map<FoodCatalog>(entity);
            await _foodCatalogRepository.InsertAsync(dataFoodCatalog);
        }

        /// <summary>
        /// Method to update a food catalog
        /// </summary>
        /// <param name="entity">Parameter with user information to update</param>
        public async Task UpdateAsync(FoodCatalogRequest entity)
        {
            FoodCatalogValidation.FoodCatalogUpdateValidate(entity);
            var dataFoodCatalog = _mapper.Map<FoodCatalog>(entity);
            await _foodCatalogRepository.UpdateAsync(dataFoodCatalog);
        } 
        #endregion
    }
}
