namespace Common.Extensions
{
    public static class Extenstions
    {
        public static bool IsNull(this object input)
        {
            return input == null;
        }

        public static bool IsNotNull(this object input)
        {
            return !input.IsNull();
        }

        //public static T Deserialize<T>(this string input)
        //{
        //    T Result = default;
        //    if (input.HasValue())
        //        Result = JsonConvert.DeserializeObject<T>(input);

        //    return Result;
        //}

        //public static string Serialize<T>(this T input)
        //{
        //    string Result = null;
        //    if (input.IsNotNull())
        //        Result = JsonConvert.SerializeObject(input);

        //    return Result;
        //}

        //public static T ToObject<T>(this string input)
        //{
        //    T Result = default;
        //    try
        //    {
        //        if (input.HasValue())
        //            Result = input.Deserialize<T>();

        //        return Result;
        //    }
        //    catch { return Result; }
        //}
    }
}
