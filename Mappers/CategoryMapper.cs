using Money.BLL.Models;
using MoneyApi.DTOs;

namespace MoneyApi.Mappers
{
    public static class CategoryMapper
    {
        //du model de la bll vers le dto de l'api
        public static CategoryDTO ToDTO(this Category model)
        {
            return new CategoryDTO
            {
                Id_Category = model.Id_Category,
                Name = model.Name,
            };
        }

        public static Category ToModel(this CategoryDataDTO category)
        {
            return new Category
            {
                Id_Category = 0, 
                Name = category.Name,
            };
        }
    }
}
