using System;

namespace InfastuctureCore.Utilities
{
    public interface IReactiveProperty<T>
    {
        public event Action<T> ValueChanged;
        public T Value { get; }
    }

    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T _value;

        private readonly Func<T, T> _valueSetter;

        public event Action<T> ValueChanged;

        public T Value
        {
            get => _value;
            set
            {
                _value = _valueSetter == null
                    ? value
                    : _valueSetter(value);

                ValueChanged?.Invoke(_value);
            }
        }

        public ReactiveProperty()
        {
            Value = default;
        }

        public ReactiveProperty(Func<T, T> valueSetter)
        {
            _valueSetter = valueSetter;
            Value = default;
        }

        public ReactiveProperty(T value, Func<T, T> valueSetter)
        {
            _valueSetter = valueSetter;
            Value = value;
        }

        public override string ToString() =>
            Value.ToString();

        public void SetValue(T value)
        {
        }
    }
}