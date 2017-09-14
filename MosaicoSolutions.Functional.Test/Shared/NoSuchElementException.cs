namespace MosaicoSolutions.Functional.Test.Shared
{
    [System.Serializable]
    public class NoSuchElementException : System.Exception
    {
        public NoSuchElementException() { }
        public NoSuchElementException(string message) : base(message) { }
        public NoSuchElementException(string message, System.Exception inner) : base(message, inner) { }
        protected NoSuchElementException(
            System.Runtime.Serialization.SerializationInfo info,
            System.Runtime.Serialization.StreamingContext context) : base(info, context) { }
    }
}