using UnityEngine;

public class InputManager : MonoBehaviour
{
    private PlayerActionMap playerActionMap;
    private Vector2 moveInput;
    // getter for the moveInput
    public Vector2 MoveInput => moveInput;

    private void Awake()
    {
        playerActionMap = new PlayerActionMap();

    }

    void OnEnable()
    {
        playerActionMap.Enable();
    }

    void OnDisable()
    {
        playerActionMap.Disable();
    }

    private void Update()
    {
        moveInput = playerActionMap.Player.Move.ReadValue<Vector2>();
    }

    
}
