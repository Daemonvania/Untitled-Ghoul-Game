using System;
using System.Security;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.iOS;

using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.ProBuilder;

[RequireComponent(typeof(NavMeshAgent))]
public class Player : MonoBehaviour
{
    [SerializeField] private InputActionAsset InputActions;
    private InputActionMap PlayerActionMap;
    private InputAction movement;
    private InputAction interact;

    [SerializeField] private Camera camera;
    private NavMeshAgent _agent;
    [SerializeField] [Range(0, 0.99f)] private float smoothing = 0.25f;
    [SerializeField] private float TargetLerpSpeed = 1;
    
    private Vector3 TargetDirection;
    private float LerpTime = 0;
    private Vector3 LastDirection;
    private Vector3 MovementVector;
    
    private bool canMove = true;
    public bool isHigh = false;
    
    [SerializeField] private Transform testing;
    
    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        PlayerActionMap = InputActions.FindActionMap("Player");
        movement = PlayerActionMap.FindAction("Move");
        interact = PlayerActionMap.FindAction("Interact");
        movement.started += HandleMovementAction;
        movement.canceled += HandleMovementAction;
        movement.performed += HandleMovementAction;
        
        interact.performed += HandleInteractions;
        
        
        PlayerActionMap.Enable();
        InputActions.Enable();
        
    }
    private void HandleMovementAction(InputAction.CallbackContext obj)
    {
        Vector2 input = obj.ReadValue<Vector2>();
        MovementVector = new Vector3(input.x, 0, input.y);
    }

    private void Update()
    {
        MovementVector.Normalize();
        if (MovementVector != LastDirection)
        {
            LerpTime = 0;
        }

        LastDirection = MovementVector;
        TargetDirection = Vector3.Lerp(TargetDirection, MovementVector, Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - smoothing)));

        _agent.Move(TargetDirection * _agent.speed * Time.deltaTime);

        Vector3 lookdirection = MovementVector;
        if (lookdirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookdirection), Mathf.Clamp01(LerpTime * TargetLerpSpeed * (1 - smoothing)));
        }

        LerpTime += Time.deltaTime;
    }

    private void HandleInteractions(InputAction.CallbackContext obj) {

        float interactDistance = 2f;
        //todo make layer for interactables
        // Debug.DrawRay(transform.position, transform.forward * 5, Color.red, 5);
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit raycastHit, interactDistance, 3)) {
            if (raycastHit.transform.TryGetComponent(out Interactable interactable)) {
                interactable.Interact(this);
            } else {
            
            }
        }
    }
    
    public void SetMovement(bool enable)
    {
        _agent.enabled = enable;
        canMove = enable;
    }
}
