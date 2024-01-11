using Money.BLL.Models;
using MoneyApi.DTOs;
using System.Reflection;

namespace MoneyApi.Mappers
{
    public static class IncomeMapper
    {
        public static IncomeDTO ToDTO(this Income model)
        {
            return new IncomeDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                Amount = model.Amount,
                Currency = model.Currency,
                PeopleId = model.PeopleId,
            };
        }

        public static Income ToModel(this IncomeDataDTO income)
        {
            return new Income
            {
                Id = 0,
                Name = income.Name,
                Description = income.Description,
                Date = income.Date,
                Amount = income.Amount,
                Currency = income.Currency,
                PeopleId = income.PeopleId,
            };
        }
    }
}
