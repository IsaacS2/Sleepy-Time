using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStats : CharacterStats
{
    [SerializeField] private VolumeProfile vp;
    [SerializeField] private float _maxDeathTime = 2f;

    private Vignette _vignette;
    protected float _restStat = 10, _restMaxStat = 10, _deathTimer;
    protected int _checkpointIndex = 0;

    public static event Action<PlayerStats> OnLevelStart = (_playerStats) => { };
    public static event Action<Vector3> OnCheckpointContact = (_checkPointPos) => { };
    public static event Action<bool> OnDeath = (_deathBool) => { };

    private void OnEnable()
    {
        if (vp) vp.TryGet(out _vignette);
        _deathTimer = _maxDeathTime;

        GameManager.SetNewPlayerPosition += MovePlayer;
    }

    private void OnDisable() { GameManager.SetNewPlayerPosition -= MovePlayer; }

    protected override void Start()
    {
        base.Start();
        OnLevelStart(this);
    }

    protected override void Update()
    {
        base.Update();

        if (_sprRend && _sprRend.color == Color.red && !_invincible) _sprRend.color = Color.white;

        if (_deathTimer < _maxDeathTime)
        {
            _deathTimer += Time.deltaTime;

            if (_deathTimer >= _maxDeathTime)
            {
                OnDeath(true);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }
    }

    public override void CalculateDamage(float damage)
    {
        base.CalculateDamage(damage);

        if (_dead && _deathTimer >= _maxDeathTime) _deathTimer = 0;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        Checkpoint checkpoint = collision.gameObject.GetComponent<Checkpoint>();

        if (checkpoint) OnCheckpointContact(collision.transform.position);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

        if (enemy)
        {
            CalculateDamage(enemy.GetStrength());
            if (_sprRend) _sprRend.color = Color.red;
        }
    }

    public void IncreaseRestStat(float increaseAmount) 
    {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);

        if (_vignette)
        {
            _vignette.intensity.value = 1 - (_restStat / _restMaxStat);
        }
    }

    public void MovePlayer(Vector3 _newPos)
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();

        if (_rb) _rb.MovePosition(_newPos);
        else transform.position = _newPos;
    }
}
