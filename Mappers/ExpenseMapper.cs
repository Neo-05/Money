using Money.BLL.Models;
using MoneyApi.DTOs;
using System.Reflection;

namespace MoneyApi.Mappers
{
    public static class ExpenseMapper
    {
        //du model de la bll vers le dto de l'api
        public static ExpenseDTO ToDTO(this Expense model)
        {
            return new ExpenseDTO
            {
                Id_Expense = model.Id_Expense,
                Name = model.Name,
                Description = model.Description,
                Date = model.Date,
                Amount = model.Amount,
                Currency = model.Currency,
                PeopleId = model.PeopleId,
                CategoryId = model.CategoryId
            };
        }

        public static Expense ToModel(this ExpenseDataDTO expense)
        {
            return new Expense
            {
                Id_Expense = 0,
                Name = expense.Name,
                Description = expense.Description,
                Date = expense.Date,
                Amount = expense.Amount,
                Currency = expense.Currency,
                PeopleId = expense.PeopleId,
                CategoryId = expense.CategoryId
            };
        }

    }
}
