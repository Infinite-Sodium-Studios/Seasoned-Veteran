using System;

public class TriggerableAction<T> : ITriggerable<T>
{
    private Action<T> action;

    public TriggerableAction(Action<T> _action)
    {
        action = _action;
    }

    public void OnTrigger(T arg)
    {
        action?.Invoke(arg);
    }
}

public class VoidTriggerableAction : ITriggerable<Nothing>
{
    private Action action;

    public VoidTriggerableAction(Action _action)
    {
        action = _action;
    }

    public void OnTrigger(Nothing arg = null)
    {
        action?.Invoke();
    }
}