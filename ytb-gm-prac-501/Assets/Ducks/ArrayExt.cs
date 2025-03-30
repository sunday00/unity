namespace Ducks
{
    public static class ArrayExt
    {
        public static void Deconstruct<T>(this T[] items, out T t0)
        {
            t0 = items.Length > 0 ? items[0] : default;
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1)
        {
            t0 = items.Length > 0 ? items[0] : default;
            t1 = items.Length > 1 ? items[1] : default;
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1, out T t2)
        {
            t0 = items.Length > 0 ? items[0] : default;
            t1 = items.Length > 1 ? items[1] : default;
            t2 = items.Length > 2 ? items[2] : default;
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1, out T t2, out T t3)
        {
            t0 = items.Length > 0 ? items[0] : default;
            t1 = items.Length > 1 ? items[1] : default;
            t2 = items.Length > 2 ? items[2] : default;
            t3 = items.Length > 3 ? items[3] : default;
        }

        public static void Deconstruct<T>(this T[] items, out T t0, out T t1, out T t2, out T t3, out T t4)
        {
            t0 = items.Length > 0 ? items[0] : default;
            t1 = items.Length > 1 ? items[1] : default;
            t2 = items.Length > 2 ? items[2] : default;
            t3 = items.Length > 3 ? items[3] : default;
            t4 = items.Length > 4 ? items[4] : default;
        }
    }
}