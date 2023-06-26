using UnityEngine;

public class ToggleOnEvent : MonoBehaviour
{
    [SerializeField] private GameEvent<bool> gameEvent;
    [SerializeField] private bool initialState;
    private TriggerableAction<bool> listener;

    void Awake()
    {
        gameObject.SetActive(initialState);
        listener = new TriggerableAction<bool>((state) => {
            var newState = initialState ^ state;
            Debug.Log("ToggleOnEvent: " + gameObject + " => " + newState);
            gameObject.SetActive(newState);
        });
        gameEvent.Add(listener);
    }

    void OnDestroy()
    {
        gameEvent.Remove(listener);
    }
}
