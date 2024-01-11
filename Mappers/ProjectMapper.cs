using Money.BLL.Models;
using MoneyApi.DTOs;
using System.Reflection;

namespace MoneyApi.Mappers
{
    public static class ProjectMapper
    {
        public static ProjectDTO ToDTO(this Project model)
        {
            return new ProjectDTO
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Amount = model.Amount,
                SpareAmount = model.SpareAmount,
                Currency = model.Currency,
                PeopleId = model.PeopleId,
            };
        }

        public static Project ToModel(this ProjectDataDTO project)
        {
            return new Project
            {
                Id = 0,
                Name = project.Name,
                Description = project.Description,
                Amount = project.Amount,
                SpareAmount = project.SpareAmount,
                Currency = project.Currency,
                PeopleId = project.PeopleId,
            };
        }
    }
}
