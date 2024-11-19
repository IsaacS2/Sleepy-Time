using System;
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

    public static event Action<Vector2> MovementChange = (_direction) => { };

    private void OnEnable()
    {
        PlayerStats.OnEntityDeath += StopMovement;
    }

    private void OnDisable()
    {
        PlayerStats.OnEntityDeath -= StopMovement;
    }

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
            AkSoundEngine.PostEvent("Play_SFX_FS_Player", gameObject);
            // They are a single sound so the event needs to be repeated every footstep.
        }
        else
        {
            // TODO: stop player-walking sound effect here
        }
        MovementChange(moveDirection);
        _rb.velocity = moveDirection * moveSpeed;
    }

    public void ChangeSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void StopMovement()
    {
        moveSpeed = 0;
    }
}
