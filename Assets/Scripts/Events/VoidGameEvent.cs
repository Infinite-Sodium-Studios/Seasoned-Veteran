using UnityEngine;

[CreateAssetMenu(fileName = "GameEvent", menuName = "Game Event/Void Game Event", order = 1)]
public class VoidGameEvent: GameEvent<Nothing>
{
    public void Trigger()
    {
        base.Trigger(null);
    }
}