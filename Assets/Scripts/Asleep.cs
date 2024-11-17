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
    public static Action<bool> OnAsleep;

    private void Start()
    {
        _player = GetComponent<PlayerStats>();
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
