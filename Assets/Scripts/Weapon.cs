using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Rigidbody2D _wielder;
    [SerializeField] private float _maxAttackTime, _weaponDistance;

    private Sprite _originalSprite;
    private Rigidbody2D _weapon;
    private Collider2D _col;
    private SpriteRenderer _spr;
    private Vector2 _weaponDirection;
    private float _distanceToPlayer, _attackTimer, _damage;
    private bool canMove;

    private void OnEnable()
    {
        Asleep.OnAsleep += ActivateWeapon;
        Awake.OnAwake += ActivateWeapon;
        GameplayInputManager.OnAttack += Attack;
        PlayerStats.OnLevelStart += EditDamage;
        canMove = true;
    }

    private void OnDisable()
    {
        Asleep.OnAsleep -= ActivateWeapon;
        Awake.OnAwake -= ActivateWeapon;
        GameplayInputManager.OnAttack -= Attack;
        PlayerStats.OnLevelStart -= EditDamage;
    }

    private void Start()
    {
        _col = GetComponent<Collider2D>();
        _spr = GetComponent<SpriteRenderer>();
        _weapon = GetComponent<Rigidbody2D>();
        if (_spr) _originalSprite = _spr.sprite;

        if (_wielder) _distanceToPlayer = _weaponDistance;

        _attackTimer = _maxAttackTime;
    }

    private void Update()
    {
        //
        // Weapon has just been used and attacking endlag is in-place
        //
        if (_attackTimer < _maxAttackTime)
        {
            _attackTimer += Time.deltaTime;

            //
            // Attack endlag is finished
            //
            if (_attackTimer >= _maxAttackTime)
            {
                _col.enabled = false;
                if (_attackSprite) _spr.sprite = _originalSprite;
                canMove = true;
            }
        }
    }

    void FixedUpdate()
    {
        if (_wielder)
        {
            if (canMove) _weaponDirection = (Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - new Vector3(0.5f, 0.5f, 0)).normalized;

            if (_weaponDirection != Vector2.zero)
            {
                //
                // Moving weapon
                //
                if (canMove) _weapon.transform.up = _weaponDirection;
                _weapon.MovePosition(_weaponDirection * _distanceToPlayer + _wielder.position);
            }
        }
    }

    public void ActivateWeapon(bool _active)
    {
        //
        // Sets the weapon's visibility based on boolean value _active; used for awake/asleep state-switching
        //
        if (_spr) _spr.enabled = _active;
    }

    public void Attack()
    {
        //
        // Attack initiated
        //
        if (_originalSprite && _spr.enabled && _col && !_col.enabled)
        {
            //
            // Attack is now in-effect
            //
            canMove = false;
            _col.enabled = true;
            _attackTimer = 0;

            // TODO: Add swinging weapon sound-effect
            AkSoundEngine.PostEvent("Play_SFX_Weapon_Swing", gameObject);

            if (_attackSprite) _spr.sprite = _attackSprite;
        }
    }

    public void EditDamage(PlayerStats _player) { _damage = _player.GetStrength(); }

    public float GetDamage() { return _damage; }
}
