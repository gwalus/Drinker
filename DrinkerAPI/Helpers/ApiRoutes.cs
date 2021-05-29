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

            public const string All = _controllerName;
            public const string ByName = _controllerName + "byName/{keyword}";
            public const string Random = _controllerName + "random";
            public const string categories = _controllerName + "categories";
            public const string glasses = _controllerName + "glasses";
            public const string byId = _controllerName + "byId/{id}";
            public const string Names = _controllerName + "names";
            public const string addCoctailAsUser = _controllerName + "addCoctail";
            public const string ingredientNames = _controllerName + "ingredientNames";
            public const string addToFavourite = _controllerName + "favourite";
            public const string getFavourites = _controllerName + "favourited";
            public const string deleteFromFavourites = _controllerName + "delete-from-favourited";
            public const string isFavourite = _controllerName + "is-favourite";
        }
        public static class Identity
        {
            private const string _controllerName = Base + "identity/";

            public const string Login = _controllerName + "login";
            public const string Register = _controllerName + "register";
        }
        public static class Admin
        {
            private const string _controllerName = Base + "Admin/";

            public const string acceptCoctail = _controllerName + "acceptCoctail";
            public const string rejectCoctail = _controllerName + "rejectCoctail";
            public const string cocktailsToAccept = _controllerName + "for-approval";
        }
    }
}
