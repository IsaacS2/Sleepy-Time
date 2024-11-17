using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Asleep : MonoBehaviour
{
    [SerializeField] private LayerMask _asleepLayer;
    [SerializeField] private float _restRate, _healthIncreaseRate, _movementSpeed;

    private PlayerStats _player;
    private Movement _movementScript;

    public static event Action<bool> OnAsleep = (_sleep) => { };

    private void Start()
    {
        _player = GetComponent<PlayerStats>();
    }

    void OnEnable()
    {
        // TODO: Add bell chimes for player as they're starting to sleep
        // TODO: Start snoring sound-effect

        // Set state to asleep
        AkSoundEngine.SetState("Sleeping", "Asleep");
        // End - TJ

        Camera.main.cullingMask = _asleepLayer;

        if (_movementScript == null) _movementScript = GetComponent<Movement>();

        if (_movementScript)
        {
            _movementScript.ChangeSpeed(_movementSpeed);
        }

        OnAsleep(false);
    }

    private void OnDisable()
    {
        // TODO: Stop snoring sound-effect (may not be necessary if it's done automatically)
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * _restRate);
        _player.CalculateDamage(-_healthIncreaseRate);
    }
}
