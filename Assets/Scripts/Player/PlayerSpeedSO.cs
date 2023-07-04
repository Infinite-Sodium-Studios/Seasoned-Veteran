using UnityEngine;
using System;

[CreateAssetMenu(fileName = "PlayerSpeed", menuName = "Player/Speed", order = 1)]
public class PlayerSpeedSO : ScriptableObject
{
    [NonSerialized] [HideInInspector] public float _speed = 0f;
    public event Action onSpeedRequest;

    public float GetSpeed()
    {
        onSpeedRequest?.Invoke();
        return _speed;
    }
}
