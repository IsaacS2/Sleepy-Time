using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private Sprite _attackSprite;
    [SerializeField] private Rigidbody2D _wielder;
    [SerializeField] private float _maxAttackTime;

    private Sprite _originalSprite;
    private Rigidbody2D _weapon;
    private Collider2D _col;
    private SpriteRenderer _spr;
    private Vector2 _weaponDirection;
    private float _distanceToPlayer, _attackTimer, _damage;

    private void OnEnable()
    {
        Asleep.OnAsleep += ActivateWeapon;
        Awake.OnAwake += ActivateWeapon;
        GameplayInputManager.OnAttack += Attack;
        PlayerStats.OnLevelStart += EditDamage;
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

        if (_wielder) _distanceToPlayer = Vector3.Distance(_wielder.position, transform.position);

        _attackTimer = _maxAttackTime;
    }

    private void Update()
    {
        if (_attackTimer < _maxAttackTime)
        {
            _attackTimer += Time.deltaTime;

            if (_attackTimer >= _maxAttackTime)
            {
                _col.enabled = false;
                if (_attackSprite) _spr.sprite = _originalSprite;
            }
        }
    }

    void FixedUpdate()
    {
        if (_wielder)
        {
            _weaponDirection = (Camera.main.ScreenToViewportPoint(Mouse.current.position.ReadValue()) - new Vector3(0.5f, 0.5f, 0)).normalized;

            if (_weaponDirection != Vector2.zero)
            {
                transform.up = _weaponDirection;
                _weapon.MovePosition(_weaponDirection * _distanceToPlayer + _wielder.position);
            }
        }
    }

    public void ActivateWeapon(bool _active)
    {
        if (_spr) _spr.enabled = _active;
    }

    public void Attack()
    {
        if (_originalSprite && _spr.enabled && _col && !_col.enabled)
        {
            _col.enabled = true;
            _attackTimer = 0;

            if (_attackSprite) _spr.sprite = _attackSprite;
        }
    }

    public void EditDamage(PlayerStats _player) { _damage = _player.GetStrength(); }

    public float GetDamage() { return _damage; }
}
