using System;
using System.Collections.Generic;
using UnityEngine;

namespace FinalGame.Develop.Utils.Reactive
{
    public class ReactiveEvent : IReadonlyEvent
    {
        private readonly List<ActionNode> _subscribers = new ();
        
        public IDisposable Subscribe(Action action)
        {
            ActionNode actionNode = new(action, Remove);
            _subscribers.Add(actionNode);
            return actionNode;
        }

        public void Invoke()
        {
            foreach (var subscriber in _subscribers)
                subscriber.Invoke();
        }

        private void Remove(ActionNode actionNode)
        {
            _subscribers.Remove(actionNode);
        }
    }

    public class ReactiveEvent<T> : IReadonlyEvent<T>
    {
        private readonly List<ActionNode<T>> _subscribers = new ();
        
        public IDisposable Subscribe(Action<T> action)
        {
            ActionNode<T> actionNode = new ActionNode<T>(action, Remove);
            _subscribers.Add(actionNode);
            return actionNode;
        }

        public void Invoke(T arg)
        {
            foreach (var subscriber in _subscribers)
                subscriber.Invoke(arg);
        }

        private void Remove(ActionNode<T> actionNode)
            => _subscribers.Remove(actionNode);
    }
}