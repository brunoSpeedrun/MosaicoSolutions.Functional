using System;

namespace MosaicoSolutions.Functional.Extensions
{
    public static class NullableExtensions
    {
        public static Option<T> ToOption<T>(this T? @this) where T: struct
            => @this.HasValue
                ? Option.Some(@this.Value)
                : Option.None<T>();

        public static void IfPresent<T>(this T? @this, Action<T> some) where T : struct
        {
            if (@this.HasValue)
                some(@this.Value);
        }

        public static void IfNone<T>(this T? @this, Action none) where T : struct
        {
            if (!@this.HasValue)
                none();
        }

        public static T GetValueOrThrow<T, TException>(this T? @this, TException exception) 
                                                       where T : struct
                                                       where TException : Exception
            => @this.GetValueOrThrow(() => exception);

        public static T GetValueOrThrow<T, TException>(this T? @this, Func<TException> exception) 
                                                       where T : struct
                                                       where TException : Exception
            => @this ?? throw exception();

        public static TResult? Map<T, TResult>(this T? @this,
                                               Func<T, TResult> mapFunc) 
                                               where T : struct
                                               where TResult : struct
            => @this.HasValue
                ? mapFunc(@this.Value)
                : new TResult?();

        public static TResult? Bind<T, TResult>(this T? @this,
                                                Func<T, TResult?> bindFunc) 
                                                where T : struct
                                                where TResult : struct
            => @this.HasValue
                ? bindFunc(@this.Value)
                : new TResult?();
        
        public static T? Where<T>(this T? @this, Func<T, bool> predicate) where T : struct
            => @this.HasValue
                ? predicate(@this.Value)
                    ? @this
                    : new T?()
                : new T?();
    }
}