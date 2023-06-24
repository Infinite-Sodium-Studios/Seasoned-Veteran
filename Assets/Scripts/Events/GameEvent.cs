using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game Event", order = 1)]
public class GameEvent : ScriptableObject
{
    List<ITriggerable> listeners = new List<ITriggerable>();

    public void Trigger()
    {
        for (int i = listeners.Count - 1; i >= 0; i--)
        {
            listeners[i].OnTrigger();
        }
    }

    public void Add(ITriggerable listener)
    {
        listeners.Add(listener);
    }

    public void Remove(ITriggerable listener)
    {
        listeners.Remove(listener);
    }
}
