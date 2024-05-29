namespace LibraryAPI
{
    public static class TableNameMapper
    {
        private static readonly Dictionary<Type, string> TableNames = new()
        {
        { typeof(Book), "db.books" },
        { typeof(Author), "db.authors" }
    };

        public static string GetTableName(Type type)
        {
            return TableNames[type];
        }
    }

}
