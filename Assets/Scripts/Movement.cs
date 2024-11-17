using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private InputActionReference movement;
    [SerializeField] private float moveSpeed;
    
    private Vector2 moveDirection;
    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = movement.action.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        //
        // Player is moving if moveDirection is not Vector2.zero
        //
        if (moveDirection != Vector2.zero)
        {
            // TODO: start player-walking sound effect here if it isn't already playing
        }
        else
        {
            // TODO: stop player-walking sound effect here
        }

        _rb.velocity = moveDirection * moveSpeed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }
}
