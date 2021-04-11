using DrinkerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    public interface ICoctailRepository
    {
        Task<ICollection<Coctail>> GetListOfCoctailsAsync();
    }
}
