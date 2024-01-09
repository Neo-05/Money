using Money.BLL.Models;
using MoneyApi.DTOs;
using System.Reflection;

namespace MoneyApi.Mappers
{
    public static class PeopleMapper
    {
        //du model de la bll vers le dto de l'api
        public static PeopleDTO ToDTO (this People model)
        {
            return new PeopleDTO
            {
                Id = model.Id,
                Pseudo = model.Pseudo,
                Email = model.Email,
                BirthDate = model.BirthDate,
                HashPwd = model.HashPwd
            };
        }

        public static People ToModel (this PeopleDataDTO people)
        {
            return new People
            {
                Id = 0, //pcq PeopleDataDTO n'a pas d'id pcq la db le fait 
                Pseudo = people.Pseudo,
                Email = people.Email,
                BirthDate = people.BirthDate,
                HashPwd = people.HashPwd
            };
        }
    }
}
