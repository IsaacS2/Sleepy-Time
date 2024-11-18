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
    private bool _stop = true;

    private void OnEnable()
    {
        PlayerStats.OnLevelStart += GetPlayer;
        PlayerStats.OnEntityDeath += JumpScare;
        Asleep.OnAsleep += SetStop;
        Awake.OnAwake += SetStop;
    }

    private void OnDisable()
    {
        PlayerStats.OnLevelStart -= GetPlayer;
        PlayerStats.OnEntityDeath -= JumpScare;
        Asleep.OnAsleep -= SetStop;
        Awake.OnAwake -= SetStop;
    }

    private void Start()
    {
        _vectorFromPlayer = _playerTransform.position - transform.position;
    }

    private void Update()
    {
        if (!_stop) {
            //
            // Entity moving
            //
            _timePast += Time.deltaTime;
        }

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
        //
        // Entity attacks player
        //

        // TODO: Add 'entity killing the player' sound effect
        AkSoundEngine.PostEvent("Play_DGX_Entity_Barks", gameObject);

        Debug.Log("Jumpscare");
    }

    public void SetStop(bool _stopEntity)
    {
        //
        // Entity stops or stars moving towards player, depending on the boolean value
        //
        _stop = _stopEntity;
    }
}
