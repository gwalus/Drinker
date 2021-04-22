using AutoMapper;
using DrinkerAPI.Dtos;
using DrinkerAPI.Models;
using System.Linq;

namespace DrinkerAPI.Helpers
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Coctail, CoctailDto>()
                .ForMember(s => s.Ingradients, d => d.MapFrom(m => m.Ingradients
                     .Select(x => new IngredientDto()
                     {
                         Name = x.Name,
                         Measure = x.Measure
                     })));
        }

    }
}
