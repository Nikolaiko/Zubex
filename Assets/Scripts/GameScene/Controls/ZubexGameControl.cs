using UnityEngine;
using UnityEngine.InputSystem;

public class ZubexGameControl : MonoBehaviour
{
    private PlayerInput input;

    public delegate void MoveActionTriggerDelegate(Vector2 moveVector);
    public event MoveActionTriggerDelegate OnMoveActionTrigger;

    public delegate void WeaponChangeActionTriggerDelegate(bool switchToNext);
    public event WeaponChangeActionTriggerDelegate OnWeaponChangeActionTrigger;

    private const string MOVE_ACTION_NAME = "Move";
    private const string NEXT_WEAPON_ACTION_NAME = "Next Weapon";
    private const string PREV_WEAPON_ACTION_NAME = "Prev Weapon";

    public void Awake() {
        input = GetComponent<PlayerInput>();
        input.onActionTriggered += inputActionTriggered;
    }

    private void inputActionTriggered(InputAction.CallbackContext obj) {
        if (obj.action.name.Equals(MOVE_ACTION_NAME)) {
            OnMoveActionTrigger?.Invoke(obj.action.ReadValue<Vector2>());
        } else if (obj.action.name.Equals(NEXT_WEAPON_ACTION_NAME) && obj.action.phase == InputActionPhase.Performed) {            
            OnWeaponChangeActionTrigger?.Invoke(true);
        } else if (obj.action.name.Equals(PREV_WEAPON_ACTION_NAME) && obj.action.phase == InputActionPhase.Performed) {
            OnWeaponChangeActionTrigger?.Invoke(false);
        }
    }
}
