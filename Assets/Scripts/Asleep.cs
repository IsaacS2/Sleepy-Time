using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Asleep : MonoBehaviour
{
    [SerializeField] private float _restRate, _healthIncreaseRate, _movementSpeed;

    private PlayerStats _player;
    private Movement _movementScript;

    public static event Action<bool> OnAsleep = (_sleep) => { };

    private void Start()
    {
        _player = GetComponent<PlayerStats>();
        AkSoundEngine.SetState("Sleeping", "Asleep");
    }

    void OnEnable()
    {
        if (_movementScript == null) _movementScript = GetComponent<Movement>();

        if (_movementScript)
        {
            _movementScript.ChangeSpeed(_movementSpeed);
        }

        OnAsleep(false);
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * _restRate);
        _player.CalculateDamage(-_healthIncreaseRate);
    }
}
