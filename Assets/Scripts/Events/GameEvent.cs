using System.Collections.Generic;
using UnityEngine;

public class GameEvent<T> : ScriptableObject
{
    List<ITriggerable<T>> listeners = new List<ITriggerable<T>>();

    public void Trigger(T arg)
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnTrigger(arg);
        }
    }

    public void Add(ITriggerable<T> listener)
    {
        listeners.Add(listener);
    }

    public void Remove(ITriggerable<T> listener)
    {
        listeners.Remove(listener);
    }
}