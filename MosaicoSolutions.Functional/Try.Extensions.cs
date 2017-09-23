using System;

namespace MosaicoSolutions.Functional.Extensions
{
    public static class Try
    {
        public static Try<T> Run<T>(Func<T> run)
        {
            try
            {
                return run();

            } catch (Exception e) {
                return e;
            }
        }

        public static Try<T> RunWithFinally<T>(Func<T> run, Action actionFinally)
        {
            try
            {
                return run();

            } catch (Exception e) {
                return e;

            } finally {
                actionFinally();
            }
        }

        public static TResult Match<T, TResult>(this Try<T> @this,
                                                Func<T, TResult> success,
                                                Func<Exception, TResult> failure)
            => @this.IsSuccess
                 ? success(@this.Success)
                 : failure(@this.Exception);

        public static TResult Match<T, TResult>(this Try<T> @this,
                                                Func<T, TResult> success,
                                                Func<TResult> failure)
            => @this.IsSuccess
                 ? success(@this.Success)
                 : failure();

        public static Try<TResult> Map<T, TResult>(this Try<T> @this, 
                                                   Func<T, TResult> map)
            => @this.IsSuccess
                ? Try.Run(() => map(@this.Success))
                : Try.None<TResult>();

        public static Try<TResult> Bind<T, TResult>(this Try<T> @this,
                                                    Func<T, Try<TResult>> bind)
            => @this.IsSuccess
                ? bind(@this.Success)
                : Try.None<TResult>();

        public static Try<T> Of<T>(T value)
            => value;

        public static Try<T> Of<T>(Option<T> value)
            => value.HasValue
                ? Try.Some(value.Value)
                : Try.None<T>();

        public static Option<T> ToOption<T>(this Try<T> @this)
            => @this.IsSuccess
                ? @this.OptionSuccess
                : Option.None<T>();
                
        internal static Try<T> None<T>()
            => new Try<T>(default(Exception));

        internal static Try<T> Some<T>(T value)
            => new Try<T>(value);

    }
}