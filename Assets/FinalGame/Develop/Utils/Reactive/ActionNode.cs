using System;

namespace FinalGame.Develop.Utils.Reactive
{
    public class ActionNode : IDisposable
    {
        private readonly Action _action;
        private readonly Action<ActionNode> _onDispose;

        public ActionNode(Action action, Action<ActionNode> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }
        
        public void Invoke() => _action?.Invoke(); 
        
        public void Dispose() => _onDispose?. Invoke(this);
    }
    
    public class ActionNode<T> : IDisposable
    {
        private readonly Action<T> _action;
        private readonly Action<ActionNode<T>> _onDispose;

        public ActionNode(Action<T> action, Action<ActionNode<T>> onDispose)
        {
            _action = action;
            _onDispose = onDispose;
        }
        
        public void Invoke(T arg) => _action?.Invoke(arg); 
        
        public void Dispose() => _onDispose?. Invoke(this);
    }
}