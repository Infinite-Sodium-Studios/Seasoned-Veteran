using System;

public class TriggerableAction : ITriggerable
{
    private Action action;

    public TriggerableAction(Action _action)
    {
        action = _action;
    }

    public void OnTrigger()
    {
        action?.Invoke();
    }
}