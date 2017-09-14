using System;
using MosaicoSolutions.Functional.Extensions;
using MosaicoSolutions.Functional.Test.Shared;
using Xunit;

namespace MosaicoSolutions.Functional.Test.OptionSamples
{
    public class MustHaveValue
    {
        private static readonly User User = new User()
        {
            Email = "user@mail.com",
            LastAccess = DateTime.Today,
            LastUpdate = DateTime.Today,
            UserName = "optionuser",
            Password = "123456"
        };

        [Theory]
        [InlineData(1)]
        [InlineData("MosaicoSolutions.Functional")]
        [InlineData(6.7)]
        public void ValueMustBePresent<T>(T value)
        {
            var option = Option.Of(value);
            Assert.True(option.HasValue);
        }
        
        [Fact]
        public void OptionUserMustHaveValue()
        {
            var option = Option.Of(User);
            Assert.True(option.HasValue);
        }
    }
}