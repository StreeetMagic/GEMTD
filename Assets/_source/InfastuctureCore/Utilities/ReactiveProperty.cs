using System;
using UnityEngine;

namespace InfastuctureCore.Utilities
{
    public class ReactiveProperty<T> : IReactiveProperty<T>
    {
        private T _value;

        private readonly Func<T, T> _valueSetter;

        public event Action<T> OnChanged;

        public T Value
        {
            get => _value;
            private set
            {
                _value = value;
                OnChanged?.Invoke(value);
                Debug.Log(_value);
            }
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
            Value = _valueSetter(value);
        }
    }

    public interface IReactiveProperty<T>
    {
        public event Action<T> OnChanged;
        public T Value { get; }
    }
}