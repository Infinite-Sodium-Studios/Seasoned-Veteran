using UnityEngine;

[RequireComponent(typeof(IPlayerController))]
class UpdatePlayerSpeedBehavior: MonoBehaviour
{
    [SerializeField] private PlayerSpeedSO playerSpeedSO;
    private IPlayerController playerController;

    void OnEnable()
    {
        playerController = GetComponent<IPlayerController>();
        playerSpeedSO.onSpeedRequest += SetSpeed;
    }

    void OnDisable()
    {
        playerSpeedSO.onSpeedRequest -= SetSpeed;
    }

    void SetSpeed()
    {
        playerSpeedSO._speed = playerController.GetSpeed();
    }
}