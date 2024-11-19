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
    [SerializeField] private float _maxDeathTime = 2f, _restMaxStat = 10;

    private Vignette _vignette;
    protected float _restStat = 10, _deathTimer;
    protected int _checkpointIndex = 0;

    public static event Action<PlayerStats> OnLevelStart = (_playerStats) => { };
    public static event Action<Vector3> OnCheckpointContact = (_checkPointPos) => { };
    public static event Action<bool> OnDeath = (_deathBool) => { };
    public static event Action OnEntityDeath = () => { };

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
        _restStat = _restMaxStat;
    }

    protected override void Update()
    {
        base.Update();

        //
        // player stops flickering after being hit
        //
        if (_sprRend && _sprRend.color == Color.red && !_invincible) _sprRend.color = Color.white;

        //
        // player is dying (death animations may be played in the meantime)
        //
        if (_deathTimer < _maxDeathTime)
        {
            _deathTimer += Time.deltaTime;

            //
            // player has died and the scene will now reload
            //
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

        //
        // start the countdown for resetting the scene after the player's death
        //
        if (_dead && _deathTimer >= _maxDeathTime)
        {
            OnEntityDeath();
            // TODO: Add player dying sound effect
            //AkSoundEngine.PostEvent("Play_DGX_Player_Scream", gameObject);
            // I opted to tie this in with the entity attack as the Play_Death_Sequence event, because I wanted the entity scream to happen in full before the player scream. 

            _deathTimer = 0;
        }
    }

    protected void OnTriggerEnter2D(Collider2D collision)
    {
        Checkpoint checkpoint = collision.gameObject.GetComponent<Checkpoint>();

        //
        // activate this checkpoint (save its position for later respawning
        //
        if (checkpoint) OnCheckpointContact(collision.transform.position);

        Entity entity = collision.gameObject.GetComponent<Entity>();

        //
        // player will be killed by the entity
        //
        if (entity)
        {
            OnEntityDeath();
            CalculateDamage(_maxHealth * 2);
        }
    }

    protected override void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("player touches their weapon");
    }

    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        EnemyStats enemy = collision.gameObject.GetComponent<EnemyStats>();

        //
        // player will take damage from the regular enemy
        //
        if (enemy)
        {
            // TODO: Add player's grunting/hurt sound effect
            AkSoundEngine.PostEvent("Play_DGX_Player_Hurt", gameObject);

            CalculateDamage(enemy.GetStrength());
            if (_sprRend) _sprRend.color = Color.red;
        }
    }

    public void IncreaseRestStat(float increaseAmount)
    {
        _restStat = Mathf.Clamp(_restStat + increaseAmount, 0, _restMaxStat);

        if (_vignette)
        {
            //
            // reduce/increase vignette value based on player's drowiness
            //
            _vignette.intensity.value = 1 - (_restStat / _restMaxStat);

            // 'Drowsiness' RTPC
            AkSoundEngine.SetRTPCValue("Drowsiness", _vignette.intensity.value);
            // End -TJ
        }
    }

    public void MovePlayer(Vector3 _newPos)
    {
        Rigidbody2D _rb = GetComponent<Rigidbody2D>();

        //
        // move the player if directional keys or WASD keys are being pushed down
        //
        if (_rb) { Debug.Log("Rigidbody found: moving to " + _newPos); _rb.position = _newPos; }
        else transform.position = _newPos;
    }
}
