using System;
using MosaicoSolutions.Functional.Extensions;
using static MosaicoSolutions.Functional.Extensions.Option;
using MosaicoSolutions.Functional.Test.Shared;
using Xunit;

namespace MosaicoSolutions.Functional.Test.OptionSamples
{
    public class ValueMustBeNotPresent
    {
        [Fact]
        public void MustBeNotPresent()
        {
            var optionUser = new Option<User>();
            Assert.False(optionUser.HasValue);
        }

        [Fact]
        public void MustThrowInvalidOperationException() 
            => Assert.Throws<InvalidOperationException>(() =>
            {
                var optionInt = None<int>();
                var value = optionInt.Value;
            });

        [Fact]
        public void ComputeIfNotPresent()
        {
            var isNotPresent = false;
            Option<string> optionString = null;
            optionString.IfNone(() => isNotPresent = true);
            Assert.True(isNotPresent);
        }
    
        [Fact]
        public void Match()
        {
            var isNotPresent = false;
            Option<float> optionFloat = None<float>();
            optionFloat.Match(
                some: _ => isNotPresent = false,
                none: () => isNotPresent = true
            );
            Assert.True(isNotPresent);
        }
    }
}