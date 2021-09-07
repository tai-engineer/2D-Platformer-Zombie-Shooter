using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(menuName ="Input/InputReader",fileName ="New Input Reader")]
public class InputReaderSO : ScriptableObject, GameInput.IPlayerInputActions
{
    GameInput _gameInput;

    public event UnityAction crouchEvent = delegate { };
    public event UnityAction jumpEvent = delegate { };
    public event UnityAction<Vector2> moveEvent = delegate { };
    public event UnityAction shootEvent = delegate { };
    public event UnityAction sprintEvent = delegate { };
    void OnEnable()
    {
        if(_gameInput == null)
        {
            _gameInput = new GameInput();
            _gameInput.PlayerInput.SetCallbacks(this);
        }

        _gameInput.PlayerInput.Enable();
    }
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            crouchEvent.Invoke(); 
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            jumpEvent.Invoke(); 
        }
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        moveEvent.Invoke(context.ReadValue<Vector2>());
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            shootEvent.Invoke(); 
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            sprintEvent.Invoke(); 
        }
    }
}
