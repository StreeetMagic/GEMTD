using System;

namespace Infrastructure.Utilities
{
  public interface IReactiveProperty<T>
  {
    public T Value { get; }
    public event Action<T> ValueChanged;
  }

  public class ReactiveProperty<T> : IReactiveProperty<T>
  {
    private readonly Func<T, T> _valueSetter;
    private T _value;

    public ReactiveProperty(T value)
    {
      Value = value;
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

    #region IReactiveProperty<T> Members

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

    #endregion

    public override string ToString() =>
      Value.ToString();
  }
}