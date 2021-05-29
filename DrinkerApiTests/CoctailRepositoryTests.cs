using DrinkerAPI.Interfaces;
using DrinkerAPI.Models;
using System;
using System.Threading.Tasks;
using Xunit;

namespace DrinkerApiTests
{
    public class CoctailRepositoryTests
    {
        public readonly ICoctailRepository _coctail;
        public CoctailRepositoryTests(ICoctailRepository coctail)
        {
            _coctail = coctail;
        }
        [Fact]
        public async Task Test1()
        {
            var expectedCoctail = new Coctail
            {
                Id = 11000,
                Name = "Mojito",
                Category = "Cocktail",
                Glass = "Highball glass",
                Instructions = "Muddle mint leaves with sugar and lime juice." +
                " Add a splash of soda water and fill the glass with cracked ice. Pour the rum and top with soda water. Garnish and serve with straw.",
                PhotoUrl = @"https://www.thecocktaildb.com/images/media/drink/metwgh1606770327.jpg",
                DateModified = "2016-11-04 09:17:09",
                IsAccepted = true,

            };
            var actualCoctail = await _coctail.GetCoctailByIdAsync(11000);
            //Assert.Equal(,actualCoctail);

        }
    }
}
