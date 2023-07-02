using UnityEngine;
using UnityEngine.Events;

public class BoolUnityEventListener : MonoBehaviour, ITriggerable<bool>
{
    public GameEvent<bool> gameEvent;
    public UnityEvent<bool> onTrigger;

    void OnEnable()
    {
        gameEvent.Add(this);
    }

    void OnDisable()
    {
        gameEvent.Remove(this);
    }

    public void OnTrigger(bool arg)
    {
        onTrigger.Invoke(arg);
    }
}
