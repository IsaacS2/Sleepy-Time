using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyStats : CharacterStats
{
    [SerializeField] private float _movementSpeed, _minDistanceToPlayer;

    private EnemySensor _sensor;
    private Rigidbody2D _rb;
    private Transform _player;

    private void OnEnable()
    {
        if (!_sensor) _sensor = GetComponentInChildren<EnemySensor>();
        _rb = GetComponent<Rigidbody2D>();
        if (_sensor) _sensor.OnPlayerFound += SetTarget;
        if (_sensor) _sensor.OnPlayerLost += ReleaseTarget;
    }

    private void OnDisable()
    {
        if (_sensor) _sensor.OnPlayerFound -= SetTarget;
        if (_sensor) _sensor.OnPlayerLost -= ReleaseTarget;
    }

    protected override void Update()
    {
        base.Update();
    }

    private void FixedUpdate()
    {
        if (_player)
        {
            float distance = Vector3.Distance(_player.position, _rb.transform.position);

            if (distance >= _minDistanceToPlayer)
            {
                //
                // Regular enemy is moving towards player
                //

                Vector3 targetDirection = (_player.position - _rb.transform.position).normalized;

                _rb.velocity = targetDirection * _movementSpeed * Time.fixedDeltaTime;
            }
        }
    }

    public override void CalculateDamage(float damage)
    {
        // TODO: Add enemy grunting/hurt sound effect

        base.CalculateDamage(damage);

        if (_dead)
        {
            // TODO: add enemy dying sound effect
            Destroy(gameObject);
        }
    }

    public void SetTarget(PlayerStats playerStats)
    {
        //
        // Regular enemy is saving player's transform for following
        //
        GameObject _playerObj = playerStats.gameObject;

        if (_playerObj.GetComponent<Rigidbody2D>()) { _player = _playerObj.GetComponent<Rigidbody2D>().transform; }
        else _player = _playerObj.transform;

        // TODO: Start playing enemy walking sound-effect
    }

    public void ReleaseTarget()
    {
        //
        // Regular enemy is releasing player's transform to stop following
        //
        _player = null;

        // TODO: Stop playing enemy walking sound-effect
    }
}
