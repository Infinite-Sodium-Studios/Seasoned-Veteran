using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
class InputActionMapBehavior: MonoBehaviour
{
    [SerializeField] private GameEvent<string> actionMapEvent;
    private PlayerInput playerInput;
    private TriggerableAction<string> actionMapListener;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
    }

    void OnEnable()
    {
        actionMapListener = new TriggerableAction<string>((newActionMap) => {
            playerInput.SwitchCurrentActionMap(newActionMap);
        });
        actionMapEvent.Add(actionMapListener);
    }

    void OnDisable()
    {
        actionMapEvent.Remove(actionMapListener);
    }
}