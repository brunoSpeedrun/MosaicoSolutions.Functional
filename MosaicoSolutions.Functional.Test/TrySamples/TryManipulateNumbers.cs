using System;
using MosaicoSolutions.Functional.Extensions;
using Xunit;

namespace MosaicoSolutions.Functional.Test.TrySamples
{
    public class TryManipulateNumbers
    {
        [Fact]
        public void TryDivideByZero()
        {
            Func<int, int, int> divide = (x, y) => x/y;
            bool result = false;

            Try.Run(() => divide(7, 0))
               .Match(success: _ => result = false,
                      failure: e => result = e is DivideByZeroException);
            
            Assert.True(result);
        }

        [Fact]
        public void TrySquare()
        {
            Func<int> multiplyChecked = () => 
            {
                Func<int, int> square = n =>
                {
                    checked
                    {
                        return n * n;
                    }
                };

                return square(int.MaxValue);
            };
            
            bool result = false;

            Try.Run(multiplyChecked)
               .Match(success: _ => result = false,
                      failure: e => result = e is ArithmeticException);
            
            Assert.True(result);
        }

    }
}