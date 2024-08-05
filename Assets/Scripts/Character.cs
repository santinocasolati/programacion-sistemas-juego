using System;
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
    private InputAction interactAction;
    #endregion

    #region Movement
    private CharacterController characterController;
    [SerializeField] private float playerSpeed;
    [SerializeField] private float rotationSpeed;
    private Vector3 movement;
    #endregion

    #region Interactions
    [SerializeField] private float interactRadius;
    [SerializeField] private LayerMask interactLayerMask;
    #endregion

    void Start()
    {
        movementAction = playerInput.actions.FindAction("Movement");

        interactAction = playerInput.actions.FindAction("Interact");
        interactAction.performed += TryInteract;

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

    private void TryInteract(InputAction.CallbackContext context)
    {
        Collider[] colliders = new Collider[5];
        int foundColliders = Physics.OverlapSphereNonAlloc(transform.position + transform.forward, interactRadius, colliders, interactLayerMask);

        if (foundColliders > 0)
        {
            foreach (Collider col in colliders)
            {
                IInteractable interaction = col.GetComponent<IInteractable>();

                if (interaction == null) continue;

                interaction.Interact();
                break;
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position + transform.forward, interactRadius);
    }
}
