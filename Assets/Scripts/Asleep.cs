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
        // TODO: I opted with a voice stinger, but we can go back to the bell if you like.

        // Set state to asleep
        AkSoundEngine.SetState("Sleeping", "Asleep");
        AkSoundEngine.PostEvent("Play_SFX_Resampled_Voice_Stinger", gameObject);
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
        AkSoundEngine.PostEvent("Play_DGX_Player_Wake", gameObject);
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * _restRate);
        _player.CalculateDamage(-_healthIncreaseRate);
    }
}
