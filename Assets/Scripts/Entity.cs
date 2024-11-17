using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [SerializeField] private float _maxArrivalTime;

    private Transform _playerTransform;
    private Vector3 _vectorFromPlayer, _startDistance;
    private float _timePast;

    private void OnEnable()
    {
        PlayerStats.OnLevelStart += GetPlayer;
        PlayerStats.OnEntityDeath += JumpScare;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelStart -= GetPlayer;
        PlayerStats.OnEntityDeath -= JumpScare;
    }

    private void Start()
    {
        _vectorFromPlayer = _playerTransform.position - transform.position;
    }

    private void Update()
    {
        _timePast += Time.deltaTime;

        transform.position = Vector3.Lerp(_playerTransform.position - _vectorFromPlayer, _playerTransform.position, _timePast / _maxArrivalTime);
    }

    public void GetPlayer(PlayerStats playerStats)
    {
        Rigidbody2D _rb = playerStats.gameObject.GetComponent<Rigidbody2D>();

        if (_rb) _playerTransform = _rb.transform;
        else _playerTransform = playerStats.gameObject.transform;
    }

    public void JumpScare()
    {
        Debug.Log("Jumpscare");
    }
}
