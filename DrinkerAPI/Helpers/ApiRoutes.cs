using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DrinkerAPI.Helpers
{
    public class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Coctails
        {
            public const string ListAll = Base + "/coctails";
            public const string ByIngredients = Base + "/Ingredients";
            public const string ByName = Base + "/{keyword}";
        }
    }
}
