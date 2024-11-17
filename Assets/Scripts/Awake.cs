using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Awake : MonoBehaviour
{
    [SerializeField] private float _restDropRate, _movementSpeed;

    private PlayerStats _player;
    private Movement _movementScript;

    public static event Action<bool> OnAwake = (_awake) => { };

    private void Start()
    {
        _player = GetComponent<PlayerStats>();

        // Set state to awake
        AkSoundEngine.SetState("Sleeping", "Awake");
        // End - TJ
    }

    void OnEnable()
    {
        if (_movementScript == null) _movementScript = GetComponent<Movement>();

        if (_movementScript)
        {
            _movementScript.ChangeSpeed(_movementSpeed);
        }

        OnAwake(true);
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * -_restDropRate);
    }
}
