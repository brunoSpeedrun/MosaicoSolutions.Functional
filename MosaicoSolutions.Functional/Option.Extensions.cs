using System;

namespace MosaicoSolutions.Functional.Extensions
{
    public static class Option
    {
        public static Option<T> Of<T>(T value) =>
            value;
        public static Option<T> Some<T>(T value) =>
            value == null
                ? throw new InvalidOperationException("Some method can not be called with a null value.")
                : value;
        public static Option<NoneType> NoneValue => 
            None<NoneType>();
        public static Option<T> None<T>() =>
            new Option<T>();
        public struct NoneType { }
        public static TResult Match<TResult, T>(this Option<T> @this,
                                                Func<T, TResult> some,
                                                Func<TResult> none) =>
            @this.HasValue
                ? some(@this.Value)
                : none();
        public static T OrElse<T>(this Option<T> @this,
                                  T other) =>
            @this.OrElse(() => other);
        public static T OrElse<T>(this Option<T> @this,
                                  Func<T> other) =>
            @this.Match(
                some: _ => _,
                none: other
            );
        public static T OrElseThrow<T, TException>(this Option<T> @this,
                                                   TException exception) 
                                                   where TException : Exception
                                                   =>
            @this.OrElseThrow(() => exception);
        public static T OrElseThrow<T, TException>(this Option<T> @this,
                                                   Func<TException> exception) 
                                                   where TException : Exception
                                                   =>
            @this.HasValue
                ? @this.Value
                : throw exception();
        public static Option<TResult> Map<TResult, T>(this Option<T> @this,
                                                      Func<T, TResult> map)
                                                      =>
            @this.HasValue
                ? map(@this.Value)
                : Option.None<TResult>();

        public static Option<TResult> Bind<TResult, T>(this Option<T> @this,
                                                      Func<T, Option<TResult>> bind)
                                                      =>
            @this.HasValue
                ? bind(@this.Value)
                : Option.None<TResult>();

        public static Option<T> Where<T>(this Option<T> @this,
                                                        Predicate<T> predicate)
                                                        =>
            @this.Match(
                none: Option.None<T>,
                some: value => predicate(value)
                                ? @this
                                : Option.None<T>()
            );                            
    }
}