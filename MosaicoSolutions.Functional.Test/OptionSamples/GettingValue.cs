using MosaicoSolutions.Functional.Extensions;
using static MosaicoSolutions.Functional.Extensions.Option;
using Xunit;

namespace MosaicoSolutions.Functional.Test.OptionSamples
{
    public class GettingValue
    {
        [Theory]
        [InlineData("Dominick Cobb")]
        [InlineData(23)] //Number 23
        [InlineData(105.6)]
        public void GettingValueFromProperty<T>(T value)
        {
            var option = Option.Of<T>(value);
            Assert.NotNull(option.Value);
        }

        [Fact]
        public void GettingValueOrDefault()
        {
            var option = NoneValue;
            var valueOrDefault = option.ValueOrDefault;
            Assert.Equal(valueOrDefault.GetType(), typeof(NoneType));
        }

        [Fact]
        public void ComputeIfPresent()
        {
            var isPresent = false;
            Option<string> optionString = "This is a SPARTA!!!";
            optionString.IfPresent(value => isPresent = value != null);
            Assert.True(isPresent);
        }

        [Theory]
        [InlineData(55)]
        [InlineData(32.4F)]
        [InlineData(91.44D)]
        [InlineData("Just a string...")]
        public void Match<T>(T value)
        {
            var isPresent = false;
            var option = Some(value);
            option.Match(
                    some: _value => isPresent = _value.Equals(value),
                    none: () => isPresent = false
                );
            Assert.True(isPresent);
        }
    }
}