using System;
using MosaicoSolutions.Functional.Extensions;
using MosaicoSolutions.Functional.Test.Shared;
using Xunit;

namespace MosaicoSolutions.Functional.Test.OptionSamples
{
    public class OptionExtensionMethods
    {
        private readonly User _otherUser = new User
        {
            Email = "user@mail.com",
            Password = "123456", //Don't make this!
        };
        
        [Fact]
        public void GettingTheOtherValue()
        {   
            Option<User> optionUser = null;

            var value = optionUser.OrElse(_otherUser);
            
            Assert.Equal(value.Email, _otherUser.Email);
            Assert.Equal(value.Password, _otherUser.Password);
        }

        [Fact]
        public void MustThrowException() 
            => Assert.Throws<NoSuchElementException>(() =>
            {
                var optionDateTime = Option.None<DateTime>();

                var value = optionDateTime.OrElseThrow(new NoSuchElementException());
            });

        [Fact]
        public void Map()
        {
            var optionUser = Option.Of(_otherUser);

            var emailAndPasswrod = optionUser.Map(value => new {value.Email, value.Password})
                                             .OrElse(() => new {Email = "", Password = ""});
            
            Assert.Equal(emailAndPasswrod.Email, _otherUser.Email);
            Assert.Equal(emailAndPasswrod.Password, _otherUser.Password);
        }

        [Fact]
        public void Bind()
        {
            Option<User> optionUser = _otherUser;

            var optionLastUpdate = optionUser.Bind(value => Option.Of(value.LastUpdate));
            
            Assert.False(optionLastUpdate.HasValue);
        }

        [Fact]
        public void Where()
        {
            var optionUser = (Option<User>) _otherUser;

            var email = optionUser.Where(user => user.Email.Contains("@"))
                                  .Map(user => user.Email)
                                  .OrElse(string.Empty);
            
            Assert.NotEmpty(email);
        }
    }
}