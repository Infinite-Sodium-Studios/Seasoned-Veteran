using UnityEngine;
using UnityEngine.Events;

public class UnityGameEventListener : MonoBehaviour, ITriggerable
{
    public GameEvent gameEvent;
    public UnityEvent onTrigger;

    void OnEnable()
    {
        gameEvent.Add(this);
    }

    void OnDisable()
    {
        gameEvent.Remove(this);
    }

    public void OnTrigger()
    {
        onTrigger.Invoke();
    }
}
