namespace CollectorsCantina.Web.Helpers
{
    public static class ApiCatalog
    {
        public static class Collection
        {
            public static string Get = "collection";
            public static string GetAll = "collections";
            public static string Put = "collection";
            public static string Post = "collection";
            public static string Delete = "collection";
            public static string Upload = "collection/uploadimage";
        }

        public static class Item
        {

            public static string Get = "item";
            public static string GetByCollection = "item/bycollection";
            public static string Search = "item/search";
            public static string Put = "item";
            public static string Post = "item";
            public static string Delete = "item";
            public static string Upload = "item/uploadimage";
        }
    }
}
