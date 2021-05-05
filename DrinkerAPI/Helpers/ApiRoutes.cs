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
            public const string ByName = _controllerName + "byName/{keyword}";
            public const string Random = _controllerName + "random";
            public const string categories = _controllerName + "categories";
            public const string glasses = _controllerName + "glasses";
            public const string byId = _controllerName + "byId/{id}";
        }
        public static class Identity
        {
            private const string _controllerName = Base + "identity/";

            public const string Login = _controllerName + "login";
            public const string Register = _controllerName + "register";
        }
    }
}
