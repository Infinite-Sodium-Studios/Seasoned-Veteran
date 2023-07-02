using UnityEngine;
using UnityEngine.Events;

public class StringUnityEventListener : MonoBehaviour, ITriggerable<string>
{
    public StringGameEvent gameEvent;
    public UnityEvent<string> onTrigger;

    void OnEnable()
    {
        gameEvent.Add(this);
    }

    void OnDisable()
    {
        gameEvent.Remove(this);
    }


    public void OnTrigger(string arg)
    {
        onTrigger.Invoke(arg);
    }
}
