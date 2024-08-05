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
    [SerializeField] private float rotationSpeed;
    private Vector3 movement;
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

        movement = new Vector3(moveInput.x, 0, moveInput.y).normalized;
        characterController.Move(movement * playerSpeed * Time.deltaTime);

        Quaternion movementRotation = Quaternion.LookRotation(movement);
        transform.rotation = Quaternion.Slerp(transform.rotation, movementRotation, rotationSpeed * Time.deltaTime);
    }
}
