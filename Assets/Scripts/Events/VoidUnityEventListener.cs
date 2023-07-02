using UnityEngine;
using UnityEngine.Events;

public class VoidUnityEventListener : MonoBehaviour, ITriggerable<Nothing>
{
    public VoidGameEvent gameEvent;
    public UnityEvent onTrigger;

    void OnEnable()
    {
        gameEvent.Add(this);
    }

    void OnDisable()
    {
        gameEvent.Remove(this);
    }

    public void OnTrigger(Nothing arg = null)
    {
        onTrigger.Invoke();
    }
}
