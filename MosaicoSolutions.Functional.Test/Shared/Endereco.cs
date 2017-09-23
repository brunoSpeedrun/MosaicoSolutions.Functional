using System.Xml.Serialization;

namespace MosaicoSolutions.Functional.Test.Shared
{
    [XmlRoot(Namespace = "", ElementName = "xmlcep")]
    public class Endereco
    {
        [XmlElement(ElementName = "cep")]
        public string Cep { get; set; }
        
        [XmlElement(ElementName = "logradouro")]
        public string Logradouro { get; set; }
        
        [XmlElement(ElementName = "complemento")]
        public string Complemento { get; set; }
        
        [XmlElement(ElementName = "bairro")]
        public string Bairro { get; set; }
        
        [XmlElement(ElementName = "localidade")]
        public string Localidade { get; set; }
        
        [XmlElement(ElementName = "uf")]
        public string Uf { get; set; }
        
        [XmlElement(ElementName = "unidade")]
        public string Unidade { get; set; }
        
        [XmlElement(ElementName = "ibge")]
        public string Ibge { get; set; }
        
        [XmlElement(ElementName = "gia")]
        public string Gia { get; set; }
    }
}