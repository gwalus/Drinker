namespace DrinkerAPI.Helpers
{
    public class ApiRoutes
    {
        public const string Root = "api/";
        public const string Version = "v1/";
        public const string Base = Root + Version;
        public static class Coctails
        {
            private const string _controllerName = Base + "cocktails/";

            public const string ListAll = _controllerName;
            public const string ByIngredients = _controllerName + "ingredients";
            public const string ByName = _controllerName + "{keyword}";
            public const string Random = _controllerName + "random";
            public const string categories = _controllerName + "categories";
            public const string glasses = _controllerName + "glasses";
        }
    }
}
