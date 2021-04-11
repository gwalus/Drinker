using DrinkerAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Interfaces
{
    interface ICoctailRepository
    {
        Task<List<Coctail>> GetListOfCoctails();
    }
}
