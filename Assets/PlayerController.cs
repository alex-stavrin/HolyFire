using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    InputAction moveAction;
    InputAction lookAction;

    void Start()
    {
        moveAction = InputSystem.actions.FindAction("Player/Move");
        lookAction = InputSystem.actions.FindAction("Player/Look");
    }

    void Update()
    {
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        Debug.Log(moveValue);

        Vector2 lookValue = lookAction.ReadValue<Vector2>();
        Debug.Log(lookValue);
    }
}
