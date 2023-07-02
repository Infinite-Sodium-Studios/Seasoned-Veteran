using System.Collections.Generic;
using UnityEngine;

public abstract class Tickable: ScriptableObject
{
    public abstract void TickMs(float deltaTimeMs);
}

public class TimerBehavior : MonoBehaviour
{
    [SerializeField] public List<Tickable> tickables;
    void FixedUpdate()
    {
        foreach (var tickable in tickables)
        {
            tickable.TickMs(Time.deltaTime * 1000f);
        }
    }
}
