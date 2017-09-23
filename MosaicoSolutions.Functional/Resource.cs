using System;
using MosaicoSolutions.Functional.Extensions;

namespace MosaicoSolutions.Functional
{
    public static class Resource
    {
        public static Func<TResource> Using<TResource>(Func<TResource> resourceSupplier)
                                                      where TResource : IDisposable
            => resourceSupplier;

        public static void Run<TResource>(this Func<TResource> resourceSupplier,
                                          Action<TResource> action)
                                          where TResource : IDisposable
        {
            var resource = resourceSupplier();
            using(resource)
                action(resource);
        }

        public static TResult Run<TResource, TResult>(this Func<TResource> resourceSupplier,
                                                      Func<TResource, TResult> func)
                                                      where TResource : IDisposable
        {
            var resource = resourceSupplier();
            using(resource)
                return func(resource);
        }

        public static Try<TResult> TryRun<TResource, TResult>(this Func<TResource> resourceSupplier,
                                                              Func<TResource, TResult> func)
                                                              where TResource : IDisposable
            => Try.Run(() => 
            {
                var resource = resourceSupplier();
                using(resource)
                    return func(resource);
            });
    }
}