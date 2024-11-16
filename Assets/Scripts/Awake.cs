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

    public static Action<bool> OnAwake;

    private void Start()
    {
        _player = GetComponent<PlayerStats>();
        _movementScript = GetComponent<Movement>();
    }

    void OnEnable()
    {
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
