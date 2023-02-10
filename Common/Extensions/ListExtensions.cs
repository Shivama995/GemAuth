namespace Common.Extensions
{
    public static class ListExtensions
    {
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            if (list.IsNotNull())
                return list.Count() <= 0;
            return true;
        }

        public static bool HasValue<T>(this IEnumerable<T> list)
        {
            return !list.IsNullOrEmpty();
        }

        public static bool IsNotNull<T>(this IEnumerable<T> list)
        {
            return !list.IsNull();
        }

        public static bool IsNull<T>(this IEnumerable<T> list)
        {
            return list == null;
        }
    }
}
