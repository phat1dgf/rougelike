using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour, InputActions.IGameplayActions
{
    private InputActions _input;

    private void Awake()
    {
        _input = GameRoot.Instance.InputSystem.Input;
    }
    private void OnEnable()
    {
        _input.Gameplay.SetCallbacks(this);
    }
    private void OnDisable()
    {
        _input.Gameplay.SetCallbacks(null);
    }
    public void OnMove(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }
}
