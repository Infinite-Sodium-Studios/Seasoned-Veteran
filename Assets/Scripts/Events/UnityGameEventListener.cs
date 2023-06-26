using UnityEngine;
using UnityEngine.Events;

public class UnityGameEventListener<T> : MonoBehaviour, ITriggerable<T>
{
    public GameEvent<T> gameEvent;
    public UnityEvent<T> onTrigger;

    void OnEnable()
    {
        gameEvent.Add(this);
    }

    void OnDisable()
    {
        gameEvent.Remove(this);
    }

    public void OnTrigger(T arg)
    {
        onTrigger.Invoke(arg);
    }
}
