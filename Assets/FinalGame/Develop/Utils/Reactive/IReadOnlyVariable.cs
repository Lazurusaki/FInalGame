using System;

namespace FinalGame.Develop.Utils.Reactive
{
    public interface IReadOnlyVariable<out T>
    {
        public event Action<T, T> Changed;

        public T Value { get; }
    }
}