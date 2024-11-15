using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Awake : MonoBehaviour
{
    [SerializeField] private float _restDropRate, _movementSpeed;

    private PlayerStats _player;

    void Start()
    {
        _player = GetComponent<PlayerStats>();

        Movement movementScript = GetComponent<Movement>();

        if (movementScript)
        {
            movementScript.ChangeSpeed(_movementSpeed);
        }
    }

    void Update()
    {
        _player.IncreaseRestStat(Time.deltaTime * -_restDropRate);
    }
}
