using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    #region Inputs
    [SerializeField] private PlayerInput playerInput;
    private InputAction movementAction;
    #endregion

    #region Movement
    private CharacterController characterController;
    [SerializeField] private float playerSpeed;
    #endregion

    void Start()
    {
        movementAction = playerInput.actions.FindAction("Movement");

        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector2 moveInput = movementAction.ReadValue<Vector2>();

        if (moveInput == Vector2.zero) return;

        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y) * playerSpeed * Time.deltaTime;
        characterController.Move(movement);
    }
}
