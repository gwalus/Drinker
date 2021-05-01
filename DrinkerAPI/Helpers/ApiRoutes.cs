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
            public const string ByIngredients = Base + "/ingredients";
            public const string ByName = Base + "/{keyword}";
            public const string Random = Base + "/coctails/random";
        }
    }
}
