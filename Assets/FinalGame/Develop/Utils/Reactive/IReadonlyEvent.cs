using System;

namespace FinalGame.Develop.Utils.Reactive
{
    public interface IReadonlyEvent
    {
        IDisposable Subscribe(Action action);
    }
    
    public interface IReadonlyEvent<T>
    {
        IDisposable Subscribe(Action<T> action);
    }
}