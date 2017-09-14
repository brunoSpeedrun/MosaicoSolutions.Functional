using System;

namespace MosaicoSolutions.Functional
{
    public struct Option<T>
    {
        private readonly T _value;

        public bool HasValue { get; }

        public T Value =>
            HasValue
                ? _value
                : throw new InvalidOperationException("No such element!");

        public T ValueOrDefault =>
            HasValue
                ? _value
                : default(T);

        internal Option(T value, bool hasValue)
        {
            _value = value;
            HasValue = hasValue;
        }

        public void IfPresent(Action<T> some)
        {
            if(HasValue)
                some(_value);
        }

        public void IfNone(Action none)
        {
            if(!HasValue)
                none();
        }
        
        public void Match(Action<T> some, Action none)
        {
            if(HasValue)
                some(_value);
            else
                none();
        }

        public static implicit operator Option<T>(T value) =>
            new Option<T>(value, value != null);
    }
}