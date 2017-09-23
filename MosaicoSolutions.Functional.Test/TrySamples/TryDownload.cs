using System;
using System.Net.Http;
using System.Xml.Linq;
using System.Xml.Serialization;
using MosaicoSolutions.Functional.Extensions;
using MosaicoSolutions.Functional.Test.Shared;
using Xunit;

namespace MosaicoSolutions.Functional.Test.TrySamples
{
    public class TryDownload
    {
        [Fact]
        public void TryDownloadString()
        {
            Func<XDocument, Endereco> XmlToEndereco = xml 
                => (Endereco) new XmlSerializer(typeof(Endereco))
                                                .Deserialize(xml.CreateReader());
            
            const string cep = "01001000";
            var optionAddress = GetAddress(cep)
                                .Map(XDocument.Parse)
                                .Map(XmlToEndereco)
                                .ToOption();
            
            Assert.True(optionAddress.HasValue);
            
            var address = optionAddress.OrElse(() => new Endereco());
            
            Assert.Equal(address.Cep.Replace("-", ""), cep);
            Assert.Equal(address.Uf, "SP");
        }

        private static Try<string> GetAddress(string cep)
            => Try.Run(() =>
            {
                using (var httpClient = new HttpClient {BaseAddress = new Uri(@"http://viacep.com.br/ws/")})
                    using (var response = httpClient.GetAsync($"{cep}/xml").Result)
                        using (var content = response.EnsureSuccessStatusCode().Content)
                            return content.ReadAsStringAsync().Result;
            });
    }
}