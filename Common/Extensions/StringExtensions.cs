using System.Text;

namespace Common.Extensions
{
    public static class StringExtensions
    {
        public static bool IsNull(this string val)
        {
            return val == null;
        }

        public static bool IsNotNull(this string val)
        {
            return !val.IsNull();
        }
        public static bool HasValue(this string val)
        {
            return !val.IsEmpty();
        }
        public static bool IsEmpty(this string val)
        {
            if (val.IsNotNull())
                return string.IsNullOrWhiteSpace(val);
            return true;
        }
        //public static T GetModelFromToken<T>(this string input)
        //{
        //    T Result = default;
        //    try
        //    {
        //        if (input.HasValue())
        //            Result = new Crypt().Decrypt<T>(input);

        //        return Result;
        //    }
        //    catch { return Result; }
        //}
        public static byte[] GetBytes(this string input)
        {
            if (!input.HasValue())
                return null;

            return Encoding.ASCII.GetBytes(input);
        }
    }
}
