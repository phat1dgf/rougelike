using System;
using System.Collections.Generic;

public class GameEventBus : IEventBus
{
    private readonly Dictionary<Type, Delegate> _events = new();

    public void Subscribe<T>(Action<T> listener)
    {
        var type = typeof(T);

        if (_events.TryGetValue(type, out var existing))
            _events[type] = Delegate.Combine(existing, listener);
        else
            _events[type] = listener;
    }

    public void Unsubscribe<T>(Action<T> listener)
    {
        var type = typeof(T);

        if (!_events.TryGetValue(type, out var existing))
            return;

        var current = Delegate.Remove(existing, listener);

        if (current == null)
            _events.Remove(type);
        else
            _events[type] = current;
    }

    public void Publish<T>(T eventData)
    {
        var type = typeof(T);

        if (_events.TryGetValue(type, out var del))
            ((Action<T>)del)?.Invoke(eventData);
    }

    public void Clear()
    {
        _events.Clear();
    }
}
