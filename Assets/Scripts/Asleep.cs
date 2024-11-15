using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asleep : MonoBehaviour
{
    [SerializeField] private float _restRate, _healthIncreaseRate, _movementSpeed;

    private PlayerStats _player;
    private Movement movementScript;

    private void Start()
    {
        _player = GetComponent<PlayerStats>();
        movementScript = GetComponent<Movement>();
    }

    void OnEnable()
    {
        if (movementScript)
        {
            movementScript.ChangeSpeed(_movementSpeed);
        }
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * _restRate);
        _player.CalculateDamage(_healthIncreaseRate);
    }
}
