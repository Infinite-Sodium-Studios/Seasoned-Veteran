using UnityEngine;
using UnityEngine.InputSystem;

namespace StarterAssets
{
    public class StarterAssetsInputs : MonoBehaviour
    {
        [Header("Tweakable Parameters")]
        private bool blockInputs = false;
        
        [Header("Character Input Values")]
        public Vector2 move;
        public Vector2 look;
        public bool jump;
        public bool sprint;
        public bool shoot;
        public int selectedWeapon = 0;

        [Header("Movement Settings")]
        public bool analogMovement;

        [Header("Mouse Settings")]
        [SerializeField] private MouseSensitivitySO sensitivity;
        public bool cursorLocked = true;
        public bool cursorInputForLook = true;

        void Start() {
            Enable();
        }

        public void OnMove(InputValue value)
        {
            MoveInput(value.Get<Vector2>());
        }

        public void OnLook(InputValue value)
        {
            if (cursorInputForLook)
            {
                LookInput(value.Get<Vector2>());
            }
        }

        public void OnJump(InputValue value)
        {
            JumpInput(value.isPressed);
        }

        public void OnSprint(InputValue value)
        {
            SprintInput(value.isPressed);
        }

        public void OnShoot(InputValue value)
        {
            ShootInput(value.isPressed);
        }

        public void OnSelectWeapon1(InputValue value)
        {
            if (value.isPressed)
            {
                SelectWeaponIndexInput(0);
            }
        }

        public void OnSelectWeapon2(InputValue value)
        {
            if (value.isPressed)
            {
                SelectWeaponIndexInput(1);
            }
        }

        public void OnSelectWeapon3(InputValue value)
        {
            if (value.isPressed)
            {
                SelectWeaponIndexInput(2);
            }
        }

        public void OnSelectWeapon4(InputValue value)
        {
            if (value.isPressed)
            {
                SelectWeaponIndexInput(3);
            }
        }

        public void MoveInput(Vector2 newMoveDirection)
        {
            move = newMoveDirection;
        }

        public void LookInput(Vector2 newLookDirection)
        {
            if (blockInputs) return;
            var mouseSensitivity = sensitivity.GetMouseSensitivity();
            newLookDirection = Vector2.Scale(newLookDirection, new Vector2(mouseSensitivity, mouseSensitivity));
            look = newLookDirection;
        }

        public void JumpInput(bool newJumpState)
        {
            jump = newJumpState;
        }

        public void SprintInput(bool newSprintState)
        {
            sprint = newSprintState;
        }

        public void ShootInput(bool newShootState)
        {
            shoot = newShootState;
        }

        public void SelectWeaponIndexInput(int weaponIndex)
        {
            if (blockInputs) return;
            selectedWeapon = weaponIndex;
        }

        private void OnApplicationFocus(bool hasFocus)
        {
            SetCursorState(cursorLocked);
        }

        private void SetCursorState(bool newState)
        {
            Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
        }

        public void Enable() {
            blockInputs = false;
        }
        
        public void Disable() {
            blockInputs = true;
        }
    }
}